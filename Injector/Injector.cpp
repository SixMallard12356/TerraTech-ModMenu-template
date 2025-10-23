#define WIN32_LEAN_AND_MEAN
#include <Windows.h>
#include <TlHelp32.h>
#include <Psapi.h>
#include <iostream>
#include <string>
#include <thread>
#include <chrono>

// 前方宣言
bool InjectDLL(DWORD processId, const std::wstring& dllPath);

// プロセス名からプロセスIDを取得
DWORD GetProcessIdByName(const std::wstring& processName)
{
    DWORD processId = 0;
    HANDLE snapshot = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);

    if (snapshot == INVALID_HANDLE_VALUE)
    {
        std::wcerr << L"[ERROR] Failed to create process snapshot" << std::endl;
        return 0;
    }

    PROCESSENTRY32W processEntry = { 0 };
    processEntry.dwSize = sizeof(PROCESSENTRY32W);

    if (Process32FirstW(snapshot, &processEntry))
    {
        do
        {
            if (processName == processEntry.szExeFile)
            {
                processId = processEntry.th32ProcessID;
                break;
            }
        } while (Process32NextW(snapshot, &processEntry));
    }

    CloseHandle(snapshot);
    return processId;
}

// 指定されたプロセスに既にDLLがインジェクトされているかチェック
bool IsAlreadyInjected(DWORD processId, const std::wstring& dllName)
{
    HANDLE hProcess = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ, FALSE, processId);
    if (!hProcess)
    {
        return false;
    }

    HMODULE hModules[1024];
    DWORD cbNeeded;

    // プロセスの全モジュールを列挙
    if (EnumProcessModules(hProcess, hModules, sizeof(hModules), &cbNeeded))
    {
        for (unsigned int i = 0; i < (cbNeeded / sizeof(HMODULE)); i++)
        {
            wchar_t szModuleName[MAX_PATH];

            // モジュール名を取得
            if (GetModuleFileNameExW(hProcess, hModules[i], szModuleName, sizeof(szModuleName) / sizeof(wchar_t)))
            {
                std::wstring modulePath = szModuleName;

                // ファイル名のみ抽出
                size_t lastSlash = modulePath.find_last_of(L"\\");
                if (lastSlash != std::wstring::npos)
                {
                    modulePath = modulePath.substr(lastSlash + 1);
                }

                // DLL名と比較（大文字小文字を区別しない）
                if (_wcsicmp(modulePath.c_str(), dllName.c_str()) == 0)
                {
                    CloseHandle(hProcess);
                    return true;
                }
            }
        }
    }

    CloseHandle(hProcess);
    return false;
}

// TerraTechプロセスの起動を待機
DWORD WaitForProcess(const std::wstring& processName, int timeoutSeconds = 0)
{
    std::wcout << L"[INFO] Waiting for " << processName << L" to start";
    if (timeoutSeconds > 0)
    {
        std::wcout << L" (timeout: " << timeoutSeconds << L"s)";
    }
    std::wcout << L"..." << std::endl;

    int elapsed = 0;
    while (true)
    {
        DWORD processId = GetProcessIdByName(processName);
        if (processId != 0)
        {
            std::wcout << L"[SUCCESS] Process found! (PID: " << processId << L")" << std::endl;
            return processId;
        }

        // タイムアウトチェック
        if (timeoutSeconds > 0 && elapsed >= timeoutSeconds)
        {
            std::wcout << L"[TIMEOUT] Process not found within " << timeoutSeconds << L" seconds" << std::endl;
            return 0;
        }

        // 1秒待機
        std::this_thread::sleep_for(std::chrono::seconds(1));
        elapsed++;

        // 進捗表示（5秒ごと）
        if (elapsed % 5 == 0)
        {
            std::wcout << L"[INFO] Still waiting... (" << elapsed << L"s elapsed)" << std::endl;
        }
    }
}

// プロセスを監視して自動的にインジェクト
void MonitorAndAutoInject(const std::wstring& processName, const std::wstring& dllPath, const std::wstring& dllName)
{
    std::wcout << L"========================================" << std::endl;
    std::wcout << L"  Auto-Inject Monitor Mode" << std::endl;
    std::wcout << L"========================================" << std::endl;
    std::wcout << L"[INFO] Monitoring for " << processName << L"..." << std::endl;
    std::wcout << L"[INFO] Press Ctrl+C to stop monitoring" << std::endl;
    std::wcout << std::endl;

    while (true)
    {
        // プロセスを検索
        DWORD processId = GetProcessIdByName(processName);

        if (processId != 0)
        {
            // 既にインジェクト済みかチェック
            if (IsAlreadyInjected(processId, dllName))
            {
                // インジェクト済みの場合は待機
                std::this_thread::sleep_for(std::chrono::seconds(5));
                continue;
            }

            std::wcout << L"[INFO] New process detected! (PID: " << processId << L")" << std::endl;
            std::wcout << L"[INFO] Starting injection..." << std::endl;

            // 少し待機（プロセスの初期化を待つ）
            std::this_thread::sleep_for(std::chrono::seconds(2));

            // インジェクト実行
            if (InjectDLL(processId, dllPath))
            {
                std::wcout << L"[SUCCESS] Auto-injection completed!" << std::endl;
                std::wcout << L"[INFO] Continuing to monitor..." << std::endl;
            }
            else
            {
                std::wcout << L"[ERROR] Auto-injection failed!" << std::endl;
            }

            std::wcout << std::endl;
        }

        // 5秒待機
        std::this_thread::sleep_for(std::chrono::seconds(5));
    }
}

// DLLをターゲットプロセスにインジェクト
bool InjectDLL(DWORD processId, const std::wstring& dllPath)
{
    std::wcout << L"[INFO] Target Process ID: " << processId << std::endl;
    std::wcout << L"[INFO] DLL Path: " << dllPath << std::endl;

    // プロセスハンドルを開く
    HANDLE hProcess = OpenProcess(PROCESS_ALL_ACCESS, FALSE, processId);
    if (!hProcess)
    {
        std::wcerr << L"[ERROR] Failed to open process. Error: " << GetLastError() << std::endl;
        return false;
    }

    std::wcout << L"[SUCCESS] Process opened successfully" << std::endl;

    // DLLパスのサイズを計算
    size_t dllPathSize = (dllPath.length() + 1) * sizeof(wchar_t);

    // ターゲットプロセスにメモリを割り当て
    LPVOID pRemoteMemory = VirtualAllocEx(hProcess, NULL, dllPathSize, MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);
    if (!pRemoteMemory)
    {
        std::wcerr << L"[ERROR] Failed to allocate memory. Error: " << GetLastError() << std::endl;
        CloseHandle(hProcess);
        return false;
    }

    std::wcout << L"[SUCCESS] Memory allocated at: 0x" << std::hex << pRemoteMemory << std::dec << std::endl;

    // DLLパスをターゲットプロセスのメモリに書き込む
    if (!WriteProcessMemory(hProcess, pRemoteMemory, dllPath.c_str(), dllPathSize, NULL))
    {
        std::wcerr << L"[ERROR] Failed to write memory. Error: " << GetLastError() << std::endl;
        VirtualFreeEx(hProcess, pRemoteMemory, 0, MEM_RELEASE);
        CloseHandle(hProcess);
        return false;
    }

    std::wcout << L"[SUCCESS] DLL path written to remote memory" << std::endl;

    // LoadLibraryW のアドレスを取得
    HMODULE hKernel32 = GetModuleHandleW(L"kernel32.dll");
    if (!hKernel32)
    {
        std::wcerr << L"[ERROR] Failed to get kernel32.dll handle" << std::endl;
        VirtualFreeEx(hProcess, pRemoteMemory, 0, MEM_RELEASE);
        CloseHandle(hProcess);
        return false;
    }

    LPTHREAD_START_ROUTINE pLoadLibraryW = (LPTHREAD_START_ROUTINE)GetProcAddress(hKernel32, "LoadLibraryW");
    if (!pLoadLibraryW)
    {
        std::wcerr << L"[ERROR] Failed to get LoadLibraryW address" << std::endl;
        VirtualFreeEx(hProcess, pRemoteMemory, 0, MEM_RELEASE);
        CloseHandle(hProcess);
        return false;
    }

    std::wcout << L"[SUCCESS] LoadLibraryW address: 0x" << std::hex << pLoadLibraryW << std::dec << std::endl;

    // リモートスレッドを作成してDLLをロード
    HANDLE hThread = CreateRemoteThread(hProcess, NULL, 0, pLoadLibraryW, pRemoteMemory, 0, NULL);
    if (!hThread)
    {
        std::wcerr << L"[ERROR] Failed to create remote thread. Error: " << GetLastError() << std::endl;
        VirtualFreeEx(hProcess, pRemoteMemory, 0, MEM_RELEASE);
        CloseHandle(hProcess);
        return false;
    }

    std::wcout << L"[SUCCESS] Remote thread created" << std::endl;

    // スレッドの終了を待つ
    WaitForSingleObject(hThread, INFINITE);

    // スレッドの終了コードを取得（DLLのモジュールハンドル）
    DWORD exitCode = 0;
    GetExitCodeThread(hThread, &exitCode);

    if (exitCode == 0)
    {
        std::wcerr << L"[ERROR] LoadLibrary failed in remote process" << std::endl;
        CloseHandle(hThread);
        VirtualFreeEx(hProcess, pRemoteMemory, 0, MEM_RELEASE);
        CloseHandle(hProcess);
        return false;
    }

    std::wcout << L"[SUCCESS] DLL loaded! Module handle: 0x" << std::hex << exitCode << std::dec << std::endl;

    // クリーンアップ
    CloseHandle(hThread);
    VirtualFreeEx(hProcess, pRemoteMemory, 0, MEM_RELEASE);
    CloseHandle(hProcess);

    return true;
}

int wmain(int argc, wchar_t* argv[])
{
    std::wcout << L"========================================" << std::endl;
    std::wcout << L"  TerraTech ModMenu Injector v2.0" << std::endl;
    std::wcout << L"========================================" << std::endl;
    std::wcout << std::endl;

    // コマンドライン引数の処理
    bool autoMode = false;
    bool waitMode = false;

    for (int i = 1; i < argc; i++)
    {
        std::wstring arg = argv[i];
        if (arg == L"-auto" || arg == L"--auto")
        {
            autoMode = true;
        }
        else if (arg == L"-wait" || arg == L"--wait")
        {
            waitMode = true;
        }
        else if (arg == L"-help" || arg == L"--help" || arg == L"-h" || arg == L"/?")
        {
            std::wcout << L"Usage: Injector.exe [options]" << std::endl;
            std::wcout << std::endl;
            std::wcout << L"Options:" << std::endl;
            std::wcout << L"  -auto   Auto-inject mode (continuously monitor and inject)" << std::endl;
            std::wcout << L"  -wait   Wait for TerraTech to start, then inject once" << std::endl;
            std::wcout << L"  -help   Show this help message" << std::endl;
            std::wcout << std::endl;
            std::wcout << L"Without options: Inject immediately if TerraTech is running" << std::endl;
            std::wcout << L"\nPress any key to exit..." << std::endl;
            std::wcin.get();
            return 0;
        }
    }

    // DLLのパスを取得（実行ファイルと同じディレクトリ）
    wchar_t exePath[MAX_PATH];
    GetModuleFileNameW(NULL, exePath, MAX_PATH);

    std::wstring exeDir = exePath;
    size_t lastSlash = exeDir.find_last_of(L"\\");
    exeDir = exeDir.substr(0, lastSlash);

    std::wstring dllPath = exeDir + L"\\Bootstrap.dll";
    std::wstring dllName = L"Bootstrap.dll";
    std::wstring processName = L"TerraTechWin64.exe";

    // DLLの存在確認
    if (GetFileAttributesW(dllPath.c_str()) == INVALID_FILE_ATTRIBUTES)
    {
        std::wcerr << L"[ERROR] Bootstrap.dll not found at: " << dllPath << std::endl;
        std::wcout << L"\nPress any key to exit..." << std::endl;
        std::wcin.get();
        return 1;
    }

    std::wcout << L"[INFO] Found Bootstrap.dll" << std::endl;
    std::wcout << std::endl;

    // モード別処理
    if (autoMode)
    {
        // 自動監視モード
        MonitorAndAutoInject(processName, dllPath, dllName);
        return 0;
    }
    else if (waitMode)
    {
        // 待機モード
        DWORD processId = WaitForProcess(processName);
        if (processId == 0)
        {
            std::wcout << L"\nPress any key to exit..." << std::endl;
            std::wcin.get();
            return 1;
        }

        // 少し待機（プロセスの初期化を待つ）
        std::wcout << L"[INFO] Waiting for process initialization..." << std::endl;
        std::this_thread::sleep_for(std::chrono::seconds(2));

        // 既にインジェクト済みかチェック
        if (IsAlreadyInjected(processId, dllName))
        {
            std::wcout << L"[INFO] Bootstrap.dll is already injected!" << std::endl;
            std::wcout << L"[INFO] Skipping injection." << std::endl;
            std::wcout << L"\nPress any key to exit..." << std::endl;
            std::wcin.get();
            return 0;
        }

        std::wcout << L"[INFO] Starting injection..." << std::endl;
        std::wcout << std::endl;

        if (InjectDLL(processId, dllPath))
        {
            std::wcout << std::endl;
            std::wcout << L"========================================" << std::endl;
            std::wcout << L"  Injection successful!" << std::endl;
            std::wcout << L"  Press INSERT in-game to open ModMenu" << std::endl;
            std::wcout << L"========================================" << std::endl;
        }
        else
        {
            std::wcerr << std::endl;
            std::wcerr << L"[ERROR] Injection failed!" << std::endl;
        }

        std::wcout << L"\nPress any key to exit..." << std::endl;
        std::wcin.get();
        return 0;
    }
    else
    {
        // スマートモード（TerraTechの状態に応じて自動的に動作を決定）
        std::wcout << L"[INFO] Searching for TerraTechWin64.exe..." << std::endl;

        DWORD processId = GetProcessIdByName(processName);

        if (processId == 0)
        {
            // TerraTechが起動していない場合、自動的に待機モードに移行
            std::wcout << L"[INFO] TerraTech is not running yet." << std::endl;
            std::wcout << L"[INFO] Automatically switching to wait mode..." << std::endl;
            std::wcout << L"\nTip: Use '-auto' option for continuous monitoring" << std::endl;
            std::wcout << std::endl;

            // 待機モードで起動を待つ
            processId = WaitForProcess(processName);
            if (processId == 0)
            {
                std::wcout << L"\nPress any key to exit..." << std::endl;
                std::wcin.get();
                return 1;
            }

            // プロセス初期化を待つ
            std::wcout << L"[INFO] Waiting for process initialization..." << std::endl;
            std::this_thread::sleep_for(std::chrono::seconds(2));
        }
        else
        {
            std::wcout << L"[SUCCESS] Found TerraTechWin64.exe (PID: " << processId << L")" << std::endl;
        }

        // 既にインジェクト済みかチェック
        if (IsAlreadyInjected(processId, dllName))
        {
            std::wcout << L"[INFO] Bootstrap.dll is already injected!" << std::endl;
            std::wcout << L"[INFO] Skipping injection." << std::endl;
            std::wcout << L"\nPress any key to exit..." << std::endl;
            std::wcin.get();
            return 0;
        }

        std::wcout << std::endl;

        // DLLをインジェクト
        std::wcout << L"[INFO] Starting injection..." << std::endl;
        std::wcout << std::endl;

        if (InjectDLL(processId, dllPath))
        {
            std::wcout << std::endl;
            std::wcout << L"========================================" << std::endl;
            std::wcout << L"  Injection successful!" << std::endl;
            std::wcout << L"  Press INSERT in-game to open ModMenu" << std::endl;
            std::wcout << L"========================================" << std::endl;
        }
        else
        {
            std::wcerr << std::endl;
            std::wcerr << L"[ERROR] Injection failed!" << std::endl;
        }

        std::wcout << L"\nPress any key to exit..." << std::endl;
        std::wcin.get();

        return 0;
    }
}
