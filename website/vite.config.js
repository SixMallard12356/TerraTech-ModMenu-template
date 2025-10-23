import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  // GitHub Pagesのベースパス設定
  // リポジトリ名が "TerraTech-ModMenu" の場合、これを使用
  // カスタムドメインを使う場合は base: '/' に変更
  base: '/TerraTech-ModMenu/',
})
