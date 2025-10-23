# GitHub Pages自動セットアップタスク

## 目的
TerraTech-ModMenuプロジェクトのGitHub Pagesを有効化して、websiteを自動デプロイできるようにする

## 前提条件
- GitHubリポジトリ: https://github.com/SixMallard12356/TerraTech-ModMenu
- ログイン済みのGitHubアカウント

## 実行タスク

### 1. GitHubリポジトリのSettings画面に移動
- URLにアクセス: https://github.com/SixMallard12356/TerraTech-ModMenu/settings/pages
- または、リポジトリのトップページから Settings > Pages に移動

### 2. Build and deployment の設定

#### Source の設定
- 「Source」ドロップダウンを見つける
- **現在の設定**: おそらく「Deploy from a branch」になっている
- **変更後**: 「**GitHub Actions**」を選択する

#### 手順
1. 「Source」のドロップダウンをクリック
2. リストから「GitHub Actions」を選択
3. 選択すると、ページが自動的に保存される

### 3. 確認

設定が完了すると、以下のメッセージが表示されるはず：
- "Your site is ready to be published at https://sixmallard12356.github.io/TerraTech-ModMenu/"
- または "Workflow details will appear here once your site has been deployed. View workflow runs."

### 4. デプロイの開始

設定完了後、以下のいずれかでデプロイがトリガーされる：
- すでにプッシュ済みの場合、自動的にワークフローが開始される
- まだの場合、次回のプッシュ時に開始される

Actions画面で確認:
- https://github.com/SixMallard12356/TerraTech-ModMenu/actions

## 期待される結果

✅ GitHub Actionsが有効化される
✅ `.github/workflows/deploy.yml` が認識される
✅ 自動ビルド＆デプロイが開始される
✅ サイトが https://sixmallard12356.github.io/TerraTech-ModMenu/ で公開される

## トラブルシューティング

### ワークフローが表示されない場合
- リポジトリに `.github/workflows/deploy.yml` が存在するか確認
- ファイルの内容が正しいか確認
- mainブランチにpushされているか確認

### デプロイに失敗する場合
- Actions画面でエラーログを確認
- `vite.config.js` の `base` 設定を確認（`/TerraTech-ModMenu/`になっているはず）

## 完了チェックリスト

- [ ] Settings > Pages で「GitHub Actions」を選択
- [ ] 設定が保存されたことを確認
- [ ] Actions画面でワークフローが実行中/完了していることを確認
- [ ] サイトURL（https://sixmallard12356.github.io/TerraTech-ModMenu/）にアクセスして表示を確認

---

**注意**: このタスクは手動でブラウザ操作が必要です。Claude for Chrome拡張機能を使用すれば、これらの操作を自動化できます。
