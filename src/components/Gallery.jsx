import { useState } from 'react';
import { motion, AnimatePresence } from 'framer-motion';
import { FaImage, FaTimes } from 'react-icons/fa';

/**
 * Galleryセクション - スクリーンショット表示
 * Modの動作画面をギャラリー形式で表示
 */
const Gallery = () => {
  const [selectedImage, setSelectedImage] = useState(null);

  // ギャラリー画像（プレースホルダー）
  const galleryImages = [
    {
      id: 1,
      title: 'God Mode & NoClip',
      description: '無敵＋壁抜けで自由に探索',
      category: 'Gameplay',
      color: 'from-blue-500 to-cyan-500',
    },
    {
      id: 2,
      title: 'Enemy ESP System',
      description: '敵CABを壁越しに可視化',
      category: 'Visual',
      color: 'from-purple-500 to-pink-500',
    },
    {
      id: 3,
      title: 'Teleport & Waypoints',
      description: 'カスタムウェイポイントシステム',
      category: 'Movement',
      color: 'from-green-500 to-emerald-500',
    },
    {
      id: 4,
      title: 'ModMenu Interface',
      description: '直感的なUI設計',
      category: 'UI',
      color: 'from-orange-500 to-red-500',
    },
    {
      id: 5,
      title: 'Auto-Aim System',
      description: 'CABへの自動エイム',
      category: 'Combat',
      color: 'from-yellow-500 to-orange-500',
      },
    {
      id: 6,
      title: 'Tech Editor Mode',
      description: '敵Techをその場で編集',
      category: 'Tech',
      color: 'from-indigo-500 to-purple-500',
    },
  ];

  return (
    <section id="gallery" className="relative py-24 px-6">
      <div className="container mx-auto max-w-7xl">
        {/* セクションヘッダー */}
        <motion.div
          initial={{ opacity: 0, y: 20 }}
          whileInView={{ opacity: 1, y: 0 }}
          viewport={{ once: true }}
          transition={{ duration: 0.6 }}
          className="text-center mb-16"
        >
          <h2 className="text-4xl md:text-5xl font-display font-bold mb-4">
            <span className="text-gradient">ギャラリー</span>
          </h2>
          <p className="text-xl text-gray-300 max-w-2xl mx-auto">
            ModMenuの動作画面をご覧ください
          </p>
        </motion.div>

        {/* ギャラリーグリッド */}
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          {galleryImages.map((image, index) => (
            <motion.div
              key={image.id}
              initial={{ opacity: 0, scale: 0.9 }}
              whileInView={{ opacity: 1, scale: 1 }}
              viewport={{ once: true }}
              transition={{ delay: index * 0.1, duration: 0.5 }}
              whileHover={{ scale: 1.05, y: -10 }}
              onClick={() => setSelectedImage(image)}
              className="group relative aspect-video glass rounded-xl overflow-hidden cursor-pointer"
            >
              {/* プレースホルダー画像（グラデーション背景） */}
              <div className={`absolute inset-0 bg-gradient-to-br ${image.color} opacity-70 group-hover:opacity-90 transition-opacity duration-300`}>
                {/* 中央アイコン */}
                <div className="absolute inset-0 flex items-center justify-center">
                  <FaImage className="text-white text-6xl opacity-30" />
                </div>
              </div>

              {/* オーバーレイテキスト */}
              <div className="absolute inset-0 bg-gradient-to-t from-black/80 via-black/40 to-transparent p-6 flex flex-col justify-end">
                {/* カテゴリバッジ */}
                <span className="inline-block px-3 py-1 bg-white bg-opacity-20 rounded-full text-xs mb-2 w-fit">
                  {image.category}
                </span>

                {/* タイトル */}
                <h3 className="text-xl font-display font-bold text-white mb-1">
                  {image.title}
                </h3>

                {/* 説明 */}
                <p className="text-sm text-gray-300">
                  {image.description}
                </p>
              </div>

              {/* ホバー時のズームアイコン */}
              <div className="absolute top-4 right-4 w-10 h-10 bg-white bg-opacity-20 rounded-full flex items-center justify-center opacity-0 group-hover:opacity-100 transition-opacity duration-300">
                <span className="text-white text-xl">🔍</span>
              </div>
            </motion.div>
          ))}
        </div>

        {/* 注意書き */}
        <motion.div
          initial={{ opacity: 0, y: 20 }}
          whileInView={{ opacity: 1, y: 0 }}
          viewport={{ once: true }}
          transition={{ delay: 0.5, duration: 0.6 }}
          className="mt-12 text-center"
        >
          <p className="text-gray-400 text-sm">
            💡 実際のスクリーンショットは後日追加予定です。現在はプレースホルダーを表示しています。
          </p>
        </motion.div>
      </div>

      {/* ライトボックス（画像拡大表示） */}
      <AnimatePresence>
        {selectedImage && (
          <motion.div
            initial={{ opacity: 0 }}
            animate={{ opacity: 1 }}
            exit={{ opacity: 0 }}
            onClick={() => setSelectedImage(null)}
            className="fixed inset-0 z-50 bg-black bg-opacity-90 flex items-center justify-center p-6"
          >
            <motion.div
              initial={{ scale: 0.8, opacity: 0 }}
              animate={{ scale: 1, opacity: 1 }}
              exit={{ scale: 0.8, opacity: 0 }}
              onClick={(e) => e.stopPropagation()}
              className="relative max-w-5xl w-full"
            >
              {/* 閉じるボタン */}
              <button
                onClick={() => setSelectedImage(null)}
                className="absolute -top-12 right-0 text-white hover:text-cyber-blue transition-colors duration-300"
              >
                <FaTimes size={32} />
              </button>

              {/* 画像プレビュー */}
              <div className={`w-full aspect-video glass rounded-2xl overflow-hidden bg-gradient-to-br ${selectedImage.color}`}>
                <div className="absolute inset-0 flex items-center justify-center">
                  <div className="text-center">
                    <FaImage className="text-white text-8xl opacity-30 mb-4 mx-auto" />
                    <h3 className="text-3xl font-display font-bold text-white mb-2">
                      {selectedImage.title}
                    </h3>
                    <p className="text-gray-300 text-lg">
                      {selectedImage.description}
                    </p>
                  </div>
                </div>
              </div>
            </motion.div>
          </motion.div>
        )}
      </AnimatePresence>
    </section>
  );
};

export default Gallery;
