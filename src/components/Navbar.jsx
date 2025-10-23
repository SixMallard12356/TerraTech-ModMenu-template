import { useState, useEffect } from 'react';
import { motion } from 'framer-motion';
import { FaGithub, FaDownload } from 'react-icons/fa';
import { Link } from 'react-scroll';

/**
 * ナビゲーションバーコンポーネント
 * スクロールに応じて背景が変化するグラスモーフィズムデザイン
 */
const Navbar = () => {
  const [scrolled, setScrolled] = useState(false);

  // スクロール時の背景変化を処理
  useEffect(() => {
    const handleScroll = () => {
      setScrolled(window.scrollY > 50);
    };

    window.addEventListener('scroll', handleScroll);
    return () => window.removeEventListener('scroll', handleScroll);
  }, []);

  // ナビゲーションメニュー項目
  const navItems = [
    { name: 'Home', to: 'hero' },
    { name: 'Features', to: 'features' },
    { name: 'Install', to: 'installation' },
    { name: 'Gallery', to: 'gallery' },
    { name: 'FAQ', to: 'faq' },
  ];

  return (
    <motion.nav
      initial={{ y: -100 }}
      animate={{ y: 0 }}
      transition={{ duration: 0.5 }}
      className={`fixed top-0 left-0 right-0 z-50 transition-all duration-300 ${
        scrolled ? 'glass glow-blue' : 'bg-transparent'
      }`}
    >
      <div className="container mx-auto px-6 py-4">
        <div className="flex items-center justify-between">
          {/* ロゴ */}
          <Link to="hero" smooth={true} duration={500} className="cursor-pointer">
            <motion.div
              whileHover={{ scale: 1.05 }}
              className="flex items-center space-x-2"
            >
              <div className="w-10 h-10 bg-gradient-to-br from-cyber-blue to-cyber-purple rounded-lg flex items-center justify-center">
                <span className="text-2xl font-bold">TT</span>
              </div>
              <span className="text-xl font-display font-bold text-gradient">
                TerraTech ModMenu
              </span>
            </motion.div>
          </Link>

          {/* ナビゲーションメニュー（デスクトップ） */}
          <div className="hidden md:flex items-center space-x-8">
            {navItems.map((item) => (
              <Link
                key={item.to}
                to={item.to}
                smooth={true}
                duration={500}
                spy={true}
                offset={-80}
                className="cursor-pointer"
              >
                <motion.span
                  whileHover={{ scale: 1.1 }}
                  className="text-white hover:text-cyber-blue transition-colors duration-300 font-medium"
                >
                  {item.name}
                </motion.span>
              </Link>
            ))}
          </div>

          {/* 右側のアクションボタン */}
          <div className="flex items-center space-x-4">
            {/* GitHubリンク */}
            <motion.a
              whileHover={{ scale: 1.1 }}
              whileTap={{ scale: 0.95 }}
              href="https://github.com/SixMallard12356/TerraTech-ModMenu"
              target="_blank"
              rel="noopener noreferrer"
              className="text-white hover:text-cyber-blue transition-colors duration-300"
            >
              <FaGithub size={24} />
            </motion.a>

            {/* ダウンロードボタン */}
            <Link to="download" smooth={true} duration={500}>
              <motion.button
                whileHover={{ scale: 1.05 }}
                whileTap={{ scale: 0.95 }}
                className="hidden md:flex items-center space-x-2 px-6 py-2 bg-gradient-to-r from-cyber-blue to-cyber-purple rounded-full font-bold hover:glow-blue transition-all duration-300"
              >
                <FaDownload />
                <span>Download</span>
              </motion.button>
            </Link>
          </div>
        </div>
      </div>
    </motion.nav>
  );
};

export default Navbar;
