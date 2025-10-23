import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  // GitHub Pagesのベースパス設定
  // カスタムドメインを使用するため base: '/' に設定
  base: '/',
})
