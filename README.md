# TerraTech ModMenu

TerraTech用のMod Menu（チートメニュー）です。DLL Injectionを使用してゲームに機能を追加します。

## 機能

- **God Mode**: 無敵モード
- **No Clip**: 壁抜け
- **Infinite Resources**: 無限リソース
- **Add Money**: お金を追加
- **Unlock All Blocks**: 全てのブロックをアンロック
- **Time Control**: ゲームスピードの変更（0.1x～5.0x）
- **Teleport**: 任意の座標にテレポート
- **Spawn Tech**: Techのスポーン
- **FPS表示**: フレームレート表示
- **Position表示**: プレイヤー座標表示

## プロジェクト構成

```
TerraTechModMenu/
├── ModMenu/           # C# DLL (実際のMod機能)
│   ├── Loader.cs      # エントリーポイント
│   ├── ModMenuManager.cs  # メニュー管理
│   └── ModMenu.csproj
├── Injector/          # C++ Injector (DLLをゲームに注入)
│   ├── Injector.cpp
│   └── Injector.vcxproj
├── Build/             # ビルド出力先
├── Libs/              # 依存ライブラリ
└── TerraTechModMenu.sln  # Visual Studioソリューション
```

## 必要な環境

- Visual Studio 2022 (C++とC#のワークロードをインストール)
- .NET Framework 4.7.2以上
- Windows 10/11 64bit
- TerraTech (Steam版)

## ビルド方法

### 方法1: Visual Studioでビルド

1. `TerraTechModMenu.sln`をVisual Studioで開く
2. ソリューション構成を`Release`、プラットフォームを`x64`に設定
3. メニューから`ビルド` -> `ソリューションのビルド`を選択
4. `Build\`フォルダに以下のファイルが生成されます：
   - `ModMenu.dll` (C# Mod本体)
   - `Injector.exe` (インジェクター)

### 方法2: コマンドラインでビルド

```bash
# ModMenu (C#) をビルド
cd C:\Programming\TerraTechModMenu\ModMenu
dotnet build -c Release

# Injector (C++) をビルド
cd C:\Programming\TerraTechModMenu\Injector
msbuild Injector.vcxproj /p:Configuration=Release /p:Platform=x64
```

## 使い方

### ステップ1: TerraTechを起動

まず、TerraTechを通常通り起動してゲームを開始します。

### ステップ2: Injectorを実行

1. `Build\`フォルダに移動
2. `Injector.exe`を**管理者権限**で実行
3. 成功すると以下のメッセージが表示されます：
   ```
   ========================================
     Injection successful!
     Press INSERT in-game to open ModMenu
   ========================================
   ```

### ステップ3: ModMenuを開く

ゲーム内で`INSERT`キーを押すとModMenuが表示されます。

### 操作方法

- `INSERT`: メニューの表示/非表示を切り替え
- `ESC`: メニューを閉じる
- マウス: メニュー内の各種機能を操作

## 注意事項

### 重要な警告

- **このModは教育目的で作成されています**
- オンラインプレイでの使用は禁止です（BANされる可能性があります）
- ゲームのアップデート後は動作しない可能性があります
- 使用は自己責任でお願いします
- セーブデータのバックアップを推奨します

### トラブルシューティング

#### Injectorが失敗する

- TerraTechが起動しているか確認
- 管理者権限で実行しているか確認
- ウイルス対策ソフトがブロックしていないか確認
- `ModMenu.dll`が`Injector.exe`と同じフォルダにあるか確認

#### メニューが表示されない

- `INSERT`キーを押したか確認
- ゲームのログファイルを確認（`%APPDATA%\..\LocalLow\Payload Studios\TerraTech\output_log.txt`）
- Unity Explorerなどのツールでロードされているか確認

#### 機能が動作しない

- 一部の機能は未実装です（`TODO`コメント参照）
- TerraTechの内部APIを解析して実装する必要があります

## 開発者向け情報

### 新しい機能を追加する

`ModMenu\ModMenuManager.cs`の`DrawWindow`メソッドに新しいUIコントロールを追加し、対応する処理を実装します。

例：
```csharp
if (GUILayout.Button("My Custom Feature"))
{
    // ここに処理を追加
    Debug.Log("Custom feature activated!");
}
```

### TerraTechのAPIを調査する

dnSpyやILSpyを使用して`Assembly-CSharp.dll`を逆コンパイルし、必要なクラス/メソッドを探します。

主要なクラス：
- `ManPlayer`: プレイヤー管理
- `Tank`: Tech（乗り物）
- `ModuleManager`: ブロック管理
- `ManSpawn`: オブジェクトのスポーン
- など

### デバッグ方法

1. Visual Studioで`ModMenu`プロジェクトを開く
2. `Debug`構成でビルド
3. Unity ExplorerやdnSpyのデバッガーをアタッチ
4. ブレークポイントを設定して動作確認

## ライセンス

このプロジェクトは教育目的で作成されています。自由に改変・配布できますが、使用は自己責任でお願いします。

## 貢献

プルリクエストやイシューは大歓迎です！

## 参考リンク

- [TerraTech 公式サイト](https://terratechgame.com/)
- [dnSpy](https://github.com/dnSpy/dnSpy) - .NETデバッガー・逆コンパイラ
- [Unity Modding Wiki](https://github.com/BepInEx/BepInEx/wiki)

## 連絡先

質問や問題があれば、GitHubのIssueで報告してください。

---

**Have fun modding TerraTech!**
