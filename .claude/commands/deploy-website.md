# GitHub Pagesサイトデプロイ自動化

このコマンドは、websiteフォルダの変更をGitHubにプッシュして、GitHub Pagesへの自動デプロイをトリガーします。

## タスク

1. **websiteフォルダの状態確認**
   - `cd website && git status` で変更があるか確認
   - 変更がない場合は、小さな変更（package.jsonのバージョンアップなど）を加える

2. **変更をコミット**
   - すべての変更ファイルを追加: `git add .`
   - わかりやすいコミットメッセージで コミット
   - コミットメッセージは日本語でOK、内容を簡潔に説明

3. **リモートにプッシュ**
   - まず `git pull --no-rebase` でリモートの変更を取得してマージ
   - コンフリクトがあれば解決（基本的にリモート優先）
   - `git push` でプッシュ

4. **GitHub Actionsの確認**
   - https://github.com/SixMallard12356/TerraTech-ModMenu/actions にアクセス
   - デプロイワークフローが開始されているか確認（URLを表示）

5. **完了メッセージ**
   - デプロイが開始されたことを確認
   - サイトURL（https://sixmallard12356.github.io/TerraTech-ModMenu/）を表示
   - 「数分後にサイトが更新されます」と伝える

## 注意事項

- websiteディレクトリは独立したgitリポジトリとして扱う
- node_modulesやdistフォルダは.gitignoreに含める
- コンフリクトが発生した場合は自動的に解決を試みる
- プッシュに失敗した場合は、エラー内容を確認して適切に対処する

## 引数

このコマンドは引数を受け取りません。実行すると自動的に最新の変更をデプロイします。
