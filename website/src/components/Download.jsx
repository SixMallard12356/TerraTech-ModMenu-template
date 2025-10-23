import { motion } from 'framer-motion';
import { FaDownload, FaGithub, FaClock, FaCheckCircle, FaExclamationTriangle } from 'react-icons/fa';

/**
 * Downloadセクション - ダウンロード情報
 * 最新バージョン情報とダウンロードリンク
 */
const Download = () => {
  // バージョン情報
  const latestVersion = {
    version: 'v1.0.0',
    releaseDate: '2025-10-18',
    size: '~2.5 MB',
    downloads: '1,234',
  };

  // 変更履歴
  const changelog = [
    { version: 'v1.0.0', date: '2025-10-18', changes: ['初回リリース', '30以上の機能を実装', 'UI/UXの最適化'] },
    { version: 'v0.9.0', date: '2025-10-15', changes: ['Beta版リリース', 'ESP機能追加', 'バグ修正'] },
    { version: 'v0.8.0', date: '2025-10-10', changes: ['Alpha版リリース', '基本機能実装', 'テスト開始'] },
  ];

  // 含まれるファイル
  const includedFiles = [
    { name: 'Injector.exe', description: 'DLL Injector（管理者権限必要）' },
    { name: 'ModMenu.dll', description: 'ModMenu本体' },
    { name: 'README.txt', description: '使い方とトラブルシューティング' },
  ];

  return (
    <section id="download" className="relative py-24 px-6 bg-gradient-to-b from-cyber-darker to-cyber-dark overflow-hidden">
      {/* 背景デコレーション */}
      <div className="absolute inset-0 opacity-5">
        <div className="absolute top-1/4 left-1/4 w-96 h-96 bg-cyber-blue rounded-full filter blur-3xl animate-pulse" />
        <div className="absolute bottom-1/4 right-1/4 w-96 h-96 bg-cyber-purple rounded-full filter blur-3xl animate-pulse" style={{ animationDelay: '1s' }} />
      </div>

      <div className="relative z-10 container mx-auto max-w-6xl">
        {/* セクションヘッダー */}
        <motion.div
          initial={{ opacity: 0, y: 20 }}
          whileInView={{ opacity: 1, y: 0 }}
          viewport={{ once: true }}
          transition={{ duration: 0.6 }}
          className="text-center mb-16"
        >
          <h2 className="text-4xl md:text-5xl font-display font-bold mb-4">
            <span className="text-gradient">ダウンロード</span>
          </h2>
          <p className="text-xl text-gray-300 max-w-2xl mx-auto">
            最新版をダウンロードして、今すぐTerraTechを支配しよう
          </p>
        </motion.div>

        <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
          {/* メインダウンロードカード */}
          <motion.div
            initial={{ opacity: 0, y: 20 }}
            whileInView={{ opacity: 1, y: 0 }}
            viewport={{ once: true }}
            transition={{ duration: 0.6 }}
            className="lg:col-span-2"
          >
            <div className="glass rounded-2xl p-8 md:p-12 relative overflow-hidden">
              {/* グロー効果 */}
              <div className="absolute top-0 right-0 w-64 h-64 bg-gradient-to-br from-cyber-blue to-cyber-purple opacity-20 blur-3xl" />

              <div className="relative z-10">
                {/* バージョン情報 */}
                <div className="flex items-center space-x-3 mb-6">
                  <span className="px-4 py-2 bg-gradient-to-r from-cyber-blue to-cyber-purple rounded-full font-bold text-sm">
                    Latest Release
                  </span>
                  <span className="text-3xl font-display font-bold">{latestVersion.version}</span>
                </div>

                {/* 統計情報 */}
                <div className="grid grid-cols-3 gap-4 mb-8">
                  <div className="text-center">
                    <FaClock className="text-cyber-blue text-2xl mx-auto mb-2" />
                    <div className="text-sm text-gray-400">リリース日</div>
                    <div className="font-bold">{latestVersion.releaseDate}</div>
                  </div>
                  <div className="text-center">
                    <div className="text-2xl mb-2">📦</div>
                    <div className="text-sm text-gray-400">ファイルサイズ</div>
                    <div className="font-bold">{latestVersion.size}</div>
                  </div>
                  <div className="text-center">
                    <FaDownload className="text-cyber-purple text-2xl mx-auto mb-2" />
                    <div className="text-sm text-gray-400">ダウンロード数</div>
                    <div className="font-bold">{latestVersion.downloads}+</div>
                  </div>
                </div>

                {/* ダウンロードボタン */}
                <div className="space-y-4">
                  <motion.a
                    whileHover={{ scale: 1.02 }}
                    whileTap={{ scale: 0.98 }}
                    href="https://github.com/SixMallard12356/TerraTech-ModMenu/releases/latest"
                    target="_blank"
                    rel="noopener noreferrer"
                    className="block w-full px-8 py-4 bg-gradient-to-r from-cyber-blue to-cyber-purple rounded-xl font-bold text-lg text-center glow-blue hover:glow-purple transition-all duration-300"
                  >
                    <div className="flex items-center justify-center space-x-3">
                      <FaDownload size={24} />
                      <span>今すぐダウンロード</span>
                    </div>
                  </motion.a>

                  <motion.a
                    whileHover={{ scale: 1.02 }}
                    whileTap={{ scale: 0.98 }}
                    href="https://github.com/SixMallard12356/TerraTech-ModMenu"
                    target="_blank"
                    rel="noopener noreferrer"
                    className="block w-full px-8 py-4 glass rounded-xl font-bold text-lg text-center hover:bg-white hover:bg-opacity-10 transition-all duration-300"
                  >
                    <div className="flex items-center justify-center space-x-3">
                      <FaGithub size={24} />
                      <span>View on GitHub</span>
                    </div>
                  </motion.a>
                </div>

                {/* 含まれるファイル */}
                <div className="mt-8">
                  <h4 className="text-lg font-bold mb-4 flex items-center">
                    <FaCheckCircle className="text-cyber-blue mr-2" />
                    含まれるファイル
                  </h4>
                  <div className="space-y-2">
                    {includedFiles.map((file, index) => (
                      <div key={index} className="flex items-start space-x-3 p-3 bg-white bg-opacity-5 rounded-lg">
                        <div className="w-2 h-2 bg-cyber-blue rounded-full mt-2" />
                        <div>
                          <div className="font-mono text-sm text-cyber-blue">{file.name}</div>
                          <div className="text-sm text-gray-400">{file.description}</div>
                        </div>
                      </div>
                    ))}
                  </div>
                </div>
              </div>
            </div>
          </motion.div>

          {/* サイドバー */}
          <div className="space-y-6">
            {/* 変更履歴 */}
            <motion.div
              initial={{ opacity: 0, x: 20 }}
              whileInView={{ opacity: 1, x: 0 }}
              viewport={{ once: true }}
              transition={{ duration: 0.6 }}
              className="glass rounded-2xl p-6"
            >
              <h3 className="text-xl font-display font-bold mb-4">変更履歴</h3>
              <div className="space-y-4">
                {changelog.map((entry, index) => (
                  <div key={index} className="pb-4 border-b border-gray-700 last:border-0">
                    <div className="flex items-center justify-between mb-2">
                      <span className="font-bold text-cyber-blue">{entry.version}</span>
                      <span className="text-xs text-gray-400">{entry.date}</span>
                    </div>
                    <ul className="text-sm text-gray-400 space-y-1">
                      {entry.changes.map((change, i) => (
                        <li key={i} className="flex items-start">
                          <span className="mr-2">•</span>
                          <span>{change}</span>
                        </li>
                      ))}
                    </ul>
                  </div>
                ))}
              </div>
            </motion.div>

            {/* 警告 */}
            <motion.div
              initial={{ opacity: 0, x: 20 }}
              whileInView={{ opacity: 1, x: 0 }}
              viewport={{ once: true }}
              transition={{ delay: 0.2, duration: 0.6 }}
              className="bg-gradient-to-br from-orange-500/20 to-red-500/20 border border-orange-500/30 rounded-xl p-6"
            >
              <div className="flex items-center space-x-2 mb-3">
                <FaExclamationTriangle className="text-orange-400" />
                <h4 className="font-bold text-orange-400">重要</h4>
              </div>
              <ul className="text-sm text-gray-300 space-y-2">
                <li>• 教育目的のみ</li>
                <li>• オンライン使用禁止</li>
                <li>• 自己責任で使用</li>
                <li>• データバックアップ推奨</li>
              </ul>
            </motion.div>
          </div>
        </div>
      </div>
    </section>
  );
};

export default Download;
