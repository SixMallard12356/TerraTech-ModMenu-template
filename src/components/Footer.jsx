import { motion } from 'framer-motion';
import { FaGithub, FaHeart, FaExclamationTriangle } from 'react-icons/fa';
import { Link } from 'react-scroll';

/**
 * Footerコンポーネント
 * サイトフッター（リンク、免責事項、コピーライト）
 */
const Footer = () => {
  // フッターリンク
  const footerLinks = {
    navigation: [
      { name: 'Home', to: 'hero' },
      { name: 'Features', to: 'features' },
      { name: 'Installation', to: 'installation' },
      { name: 'Gallery', to: 'gallery' },
      { name: 'Download', to: 'download' },
      { name: 'FAQ', to: 'faq' },
    ],
    resources: [
      { name: 'GitHub Repository', url: 'https://github.com/SixMallard12356/TerraTech-ModMenu' },
      { name: 'Report Issues', url: 'https://github.com/SixMallard12356/TerraTech-ModMenu/issues' },
      { name: 'Changelog', url: 'https://github.com/SixMallard12356/TerraTech-ModMenu/releases' },
    ],
  };

  return (
    <footer className="relative bg-gradient-to-b from-cyber-dark to-black pt-16 pb-8 px-6">
      {/* 上部のグラデーション線 */}
      <div className="absolute top-0 left-0 right-0 h-px bg-gradient-to-r from-transparent via-cyber-blue to-transparent" />

      <div className="container mx-auto max-w-7xl">
        {/* メインコンテンツ */}
        <div className="grid grid-cols-1 md:grid-cols-3 gap-12 mb-12">
          {/* ロゴ＆説明 */}
          <div>
            <motion.div
              initial={{ opacity: 0, y: 20 }}
              whileInView={{ opacity: 1, y: 0 }}
              viewport={{ once: true }}
              transition={{ duration: 0.5 }}
            >
              <div className="flex items-center space-x-2 mb-4">
                <div className="w-10 h-10 bg-gradient-to-br from-cyber-blue to-cyber-purple rounded-lg flex items-center justify-center">
                  <span className="text-2xl font-bold">TT</span>
                </div>
                <span className="text-xl font-display font-bold text-gradient">
                  TerraTech ModMenu
                </span>
              </div>
              <p className="text-gray-400 text-sm leading-relaxed">
                TerraTech用の究極のModMenu。30以上の強力な機能で、ゲーム体験を完全に変革します。
              </p>
            </motion.div>
          </div>

          {/* ナビゲーション */}
          <div>
            <motion.div
              initial={{ opacity: 0, y: 20 }}
              whileInView={{ opacity: 1, y: 0 }}
              viewport={{ once: true }}
              transition={{ delay: 0.1, duration: 0.5 }}
            >
              <h3 className="text-lg font-display font-bold mb-4 text-white">ナビゲーション</h3>
              <ul className="space-y-2">
                {footerLinks.navigation.map((link, index) => (
                  <li key={index}>
                    <Link
                      to={link.to}
                      smooth={true}
                      duration={500}
                      className="text-gray-400 hover:text-cyber-blue transition-colors duration-300 cursor-pointer text-sm"
                    >
                      {link.name}
                    </Link>
                  </li>
                ))}
              </ul>
            </motion.div>
          </div>

          {/* リソース */}
          <div>
            <motion.div
              initial={{ opacity: 0, y: 20 }}
              whileInView={{ opacity: 1, y: 0 }}
              viewport={{ once: true }}
              transition={{ delay: 0.2, duration: 0.5 }}
            >
              <h3 className="text-lg font-display font-bold mb-4 text-white">リソース</h3>
              <ul className="space-y-2">
                {footerLinks.resources.map((link, index) => (
                  <li key={index}>
                    <a
                      href={link.url}
                      target="_blank"
                      rel="noopener noreferrer"
                      className="text-gray-400 hover:text-cyber-blue transition-colors duration-300 text-sm"
                    >
                      {link.name}
                    </a>
                  </li>
                ))}
              </ul>
            </motion.div>
          </div>
        </div>

        {/* 免責事項 */}
        <motion.div
          initial={{ opacity: 0, y: 20 }}
          whileInView={{ opacity: 1, y: 0 }}
          viewport={{ once: true }}
          transition={{ delay: 0.3, duration: 0.5 }}
          className="mb-8 p-6 bg-gradient-to-r from-orange-500/10 to-red-500/10 border border-orange-500/20 rounded-xl"
        >
          <div className="flex items-start space-x-3">
            <FaExclamationTriangle className="text-orange-400 flex-shrink-0 mt-1" />
            <div>
              <h4 className="font-bold text-orange-400 mb-2">免責事項</h4>
              <p className="text-sm text-gray-400 leading-relaxed">
                このModMenuは教育目的で作成されています。オンラインプレイでの使用は禁止です。使用は完全に自己責任で行ってください。
                開発者は、このツールの使用によって生じたいかなる損害についても責任を負いません。
                ゲームのアップデート後は動作しない可能性があります。セーブデータのバックアップを強く推奨します。
              </p>
            </div>
          </div>
        </motion.div>

        {/* 区切り線 */}
        <div className="h-px bg-gradient-to-r from-transparent via-gray-700 to-transparent mb-8" />

        {/* コピーライト */}
        <motion.div
          initial={{ opacity: 0 }}
          whileInView={{ opacity: 1 }}
          viewport={{ once: true }}
          transition={{ delay: 0.4, duration: 0.5 }}
          className="flex flex-col md:flex-row items-center justify-between text-sm text-gray-400"
        >
          <div className="flex items-center space-x-1 mb-4 md:mb-0">
            <span>Made with</span>
            <FaHeart className="text-red-500 animate-pulse" />
            <span>for TerraTech Community</span>
          </div>

          <div className="flex items-center space-x-6">
            <span>© 2025 TerraTech ModMenu</span>
            <a
              href="https://github.com/SixMallard12356/TerraTech-ModMenu"
              target="_blank"
              rel="noopener noreferrer"
              className="hover:text-cyber-blue transition-colors duration-300"
            >
              <FaGithub size={20} />
            </a>
          </div>
        </motion.div>

        {/* 追加情報 */}
        <motion.div
          initial={{ opacity: 0 }}
          whileInView={{ opacity: 1 }}
          viewport={{ once: true }}
          transition={{ delay: 0.5, duration: 0.5 }}
          className="mt-6 text-center text-xs text-gray-500"
        >
          <p>
            TerraTechは Payload Studios の商標です。このプロジェクトは Payload Studios と公式に提携しているものではありません。
          </p>
        </motion.div>
      </div>
    </footer>
  );
};

export default Footer;
