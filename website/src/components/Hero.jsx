import { useCallback } from 'react';
import { motion } from 'framer-motion';
import Particles from '@tsparticles/react';
import { loadSlim } from '@tsparticles/slim';
import { FaDownload, FaGithub, FaRocket } from 'react-icons/fa';
import { Link } from 'react-scroll';

/**
 * Heroセクション - メインビジュアル
 * パーティクル背景とキャッチーなタイトル
 */
const Hero = () => {
  // パーティクルエンジンの初期化
  const particlesInit = useCallback(async (engine) => {
    await loadSlim(engine);
  }, []);

  // パーティクル設定
  const particlesOptions = {
    background: {
      color: {
        value: 'transparent',
      },
    },
    fpsLimit: 120,
    interactivity: {
      events: {
        onHover: {
          enable: true,
          mode: 'repulse',
        },
        resize: true,
      },
      modes: {
        repulse: {
          distance: 100,
          duration: 0.4,
        },
      },
    },
    particles: {
      color: {
        value: ['#00d4ff', '#a855f7', '#ff6b35'],
      },
      links: {
        color: '#00d4ff',
        distance: 150,
        enable: true,
        opacity: 0.3,
        width: 1,
      },
      move: {
        direction: 'none',
        enable: true,
        outModes: {
          default: 'bounce',
        },
        random: false,
        speed: 1,
        straight: false,
      },
      number: {
        density: {
          enable: true,
          area: 800,
        },
        value: 80,
      },
      opacity: {
        value: 0.5,
      },
      shape: {
        type: 'circle',
      },
      size: {
        value: { min: 1, max: 3 },
      },
    },
    detectRetina: true,
  };

  return (
    <section
      id="hero"
      className="relative min-h-screen flex items-center justify-center overflow-hidden"
    >
      {/* パーティクル背景 */}
      <Particles
        id="tsparticles"
        init={particlesInit}
        options={particlesOptions}
        className="absolute inset-0 z-0"
      />

      {/* グラデーションオーバーレイ */}
      <div className="absolute inset-0 bg-gradient-to-b from-transparent via-cyber-darker/50 to-cyber-dark z-0" />

      {/* コンテンツ */}
      <div className="relative z-10 container mx-auto px-6 py-32 text-center">
        <motion.div
          initial={{ opacity: 0, y: 30 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ duration: 0.8 }}
        >
          {/* バッジ */}
          <motion.div
            initial={{ opacity: 0, scale: 0.8 }}
            animate={{ opacity: 1, scale: 1 }}
            transition={{ delay: 0.2, duration: 0.5 }}
            className="inline-flex items-center space-x-2 px-4 py-2 glass rounded-full mb-8"
          >
            <FaRocket className="text-cyber-blue" />
            <span className="text-sm font-medium">v1.0.0 - Ultimate ModMenu</span>
          </motion.div>

          {/* メインタイトル */}
          <motion.h1
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ delay: 0.4, duration: 0.8 }}
            className="text-5xl md:text-7xl font-display font-bold mb-6"
          >
            <span className="text-gradient">TerraTech</span>
            <br />
            <span className="text-white">を支配せよ</span>
          </motion.h1>

          {/* サブタイトル */}
          <motion.p
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ delay: 0.6, duration: 0.8 }}
            className="text-xl md:text-2xl text-gray-300 mb-12 max-w-3xl mx-auto"
          >
            30以上の強力な機能を搭載した究極のModMenu
            <br />
            God Mode、Auto-Aim、Teleport、ESP、そしてさらに...
          </motion.p>

          {/* CTAボタン */}
          <motion.div
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ delay: 0.8, duration: 0.8 }}
            className="flex flex-col sm:flex-row items-center justify-center space-y-4 sm:space-y-0 sm:space-x-6"
          >
            {/* ダウンロードボタン */}
            <Link to="download" smooth={true} duration={500}>
              <motion.button
                whileHover={{ scale: 1.05 }}
                whileTap={{ scale: 0.95 }}
                className="px-8 py-4 bg-gradient-to-r from-cyber-blue to-cyber-purple rounded-full font-bold text-lg flex items-center space-x-3 glow-blue hover:glow-purple transition-all duration-300"
              >
                <FaDownload size={20} />
                <span>今すぐダウンロード</span>
              </motion.button>
            </Link>

            {/* GitHubボタン */}
            <motion.a
              whileHover={{ scale: 1.05 }}
              whileTap={{ scale: 0.95 }}
              href="https://github.com/SixMallard12356/TerraTech-ModMenu"
              target="_blank"
              rel="noopener noreferrer"
              className="px-8 py-4 glass rounded-full font-bold text-lg flex items-center space-x-3 hover:bg-white hover:bg-opacity-20 transition-all duration-300"
            >
              <FaGithub size={20} />
              <span>View on GitHub</span>
            </motion.a>
          </motion.div>

          {/* スクロール促進アニメーション */}
          <motion.div
            initial={{ opacity: 0 }}
            animate={{ opacity: 1 }}
            transition={{ delay: 1.2, duration: 0.8 }}
            className="mt-20"
          >
            <Link to="features" smooth={true} duration={500}>
              <motion.div
                animate={{
                  y: [0, 10, 0],
                }}
                transition={{
                  duration: 1.5,
                  repeat: Infinity,
                  ease: 'easeInOut',
                }}
                className="inline-block cursor-pointer"
              >
                <div className="w-6 h-10 border-2 border-cyber-blue rounded-full flex items-start justify-center p-2">
                  <motion.div
                    animate={{
                      y: [0, 12, 0],
                    }}
                    transition={{
                      duration: 1.5,
                      repeat: Infinity,
                      ease: 'easeInOut',
                    }}
                    className="w-1.5 h-1.5 bg-cyber-blue rounded-full"
                  />
                </div>
              </motion.div>
            </Link>
          </motion.div>
        </motion.div>
      </div>
    </section>
  );
};

export default Hero;
