#define WIN32_LEAN_AND_MEAN
#include <Windows.h>
#include <string>
#include <iostream>
#include <fstream>

// Mono API の型定義
typedef void* (*mono_thread_attach_func)(void*);
typedef void* (*mono_get_root_domain_func)();
typedef void* (*mono_domain_assembly_open_func)(void*, const char*);
typedef void* (*mono_assembly_get_image_func)(void*);
typedef void* (*mono_class_from_name_func)(void*, const char*, const char*);
typedef void* (*mono_class_get_method_from_name_func)(void*, const char*, int);
typedef void* (*mono_runtime_invoke_func)(void*, void*, void**, void**);

// グローバル変数
HMODULE hMono = nullptr;

// ログファイルへの出力
void Log(const std::string& message)
{
    std::ofstream logFile("C:\\Programming\\TerraTechModMenu\\bootstrap_log.txt", std::ios::app);
    if (logFile.is_open())
    {
        logFile << message << std::endl;
        logFile.close();
    }
}

// C#のLoader.Load()メソッドを呼び出す
void LoadModMenu()
{
    Log("[Bootstrap] Starting ModMenu loading process...");

    // mono-2.0-bdwgc.dll をロード
    hMono = GetModuleHandleA("mono-2.0-bdwgc.dll");
    if (!hMono)
    {
        Log("[Bootstrap] ERROR: Failed to get mono-2.0-bdwgc.dll handle");
        return;
    }
    Log("[Bootstrap] Successfully got mono-2.0-bdwgc.dll handle");

    // Mono API関数を取得
    auto mono_thread_attach = (mono_thread_attach_func)GetProcAddress(hMono, "mono_thread_attach");
    auto mono_get_root_domain = (mono_get_root_domain_func)GetProcAddress(hMono, "mono_get_root_domain");
    auto mono_domain_assembly_open = (mono_domain_assembly_open_func)GetProcAddress(hMono, "mono_domain_assembly_open");
    auto mono_assembly_get_image = (mono_assembly_get_image_func)GetProcAddress(hMono, "mono_assembly_get_image");
    auto mono_class_from_name = (mono_class_from_name_func)GetProcAddress(hMono, "mono_class_from_name");
    auto mono_class_get_method_from_name = (mono_class_get_method_from_name_func)GetProcAddress(hMono, "mono_class_get_method_from_name");
    auto mono_runtime_invoke = (mono_runtime_invoke_func)GetProcAddress(hMono, "mono_runtime_invoke");

    if (!mono_thread_attach || !mono_get_root_domain || !mono_domain_assembly_open ||
        !mono_assembly_get_image || !mono_class_from_name || !mono_class_get_method_from_name ||
        !mono_runtime_invoke)
    {
        Log("[Bootstrap] ERROR: Failed to get Mono API functions");
        return;
    }
    Log("[Bootstrap] Successfully got all Mono API functions");

    // Monoドメインを取得
    void* domain = mono_get_root_domain();
    if (!domain)
    {
        Log("[Bootstrap] ERROR: Failed to get root domain");
        return;
    }
    Log("[Bootstrap] Successfully got root domain");

    // 現在のスレッドをMonoにアタッチ
    mono_thread_attach(domain);
    Log("[Bootstrap] Thread attached to Mono domain");

    // ModMenu.dll のパスを取得
    char dllPath[MAX_PATH];
    GetModuleFileNameA(nullptr, dllPath, MAX_PATH);
    std::string path = dllPath;
    size_t lastSlash = path.find_last_of("\\/");
    path = path.substr(0, lastSlash);
    path += "\\ModMenu.dll";

    Log("[Bootstrap] ModMenu.dll path: " + path);

    // ModMenu.dll をロード
    void* assembly = mono_domain_assembly_open(domain, path.c_str());
    if (!assembly)
    {
        Log("[Bootstrap] ERROR: Failed to load ModMenu.dll assembly");
        return;
    }
    Log("[Bootstrap] Successfully loaded ModMenu.dll assembly");

    // アセンブリからイメージを取得
    void* image = mono_assembly_get_image(assembly);
    if (!image)
    {
        Log("[Bootstrap] ERROR: Failed to get assembly image");
        return;
    }
    Log("[Bootstrap] Successfully got assembly image");

    // Loaderクラスを取得
    void* loaderClass = mono_class_from_name(image, "TerraTechModMenu", "Loader");
    if (!loaderClass)
    {
        Log("[Bootstrap] ERROR: Failed to get Loader class");
        return;
    }
    Log("[Bootstrap] Successfully got Loader class");

    // Load()メソッドを取得
    void* loadMethod = mono_class_get_method_from_name(loaderClass, "Load", 0);
    if (!loadMethod)
    {
        Log("[Bootstrap] ERROR: Failed to get Load method");
        return;
    }
    Log("[Bootstrap] Successfully got Load method");

    // Load()メソッドを実行
    void* exception = nullptr;
    mono_runtime_invoke(loadMethod, nullptr, nullptr, &exception);

    if (exception)
    {
        Log("[Bootstrap] ERROR: Exception occurred while invoking Load method");
        return;
    }

    Log("[Bootstrap] ========================================");
    Log("[Bootstrap] ModMenu successfully loaded!");
    Log("[Bootstrap] ========================================");
}

// DLLエントリーポイント
BOOL APIENTRY DllMain(HMODULE hModule, DWORD ul_reason_for_call, LPVOID lpReserved)
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
        DisableThreadLibraryCalls(hModule);

        // 新しいスレッドでModMenuをロード
        CreateThread(nullptr, 0, [](LPVOID) -> DWORD {
            // ゲームが完全に初期化されるまで少し待つ
            Sleep(1000);
            LoadModMenu();
            return 0;
        }, nullptr, 0, nullptr);
        break;

    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}
