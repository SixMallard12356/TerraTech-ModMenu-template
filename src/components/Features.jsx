import { motion } from 'framer-motion';
import {
  FaShieldAlt,
  FaCrosshairs,
  FaBolt,
  FaRocket,
  FaCoins,
  FaEye,
  FaCog,
  FaClock,
  FaMapMarkedAlt,
  FaRobot,
  FaFire,
  FaCube,
} from 'react-icons/fa';

/**
 * Featuresセクション - 機能一覧
 * ModMenuの主要機能をカード形式で表示
 */
const Features = () => {
  // 機能リスト（カテゴリ別）
  const features = [
    {
      icon: FaShieldAlt,
      title: 'God Mode',
      description: '無敵モード - ダメージを一切受けなくなります',
      category: 'Combat',
      color: 'from-green-400 to-emerald-600',
    },
    {
      icon: FaCrosshairs,
      title: 'Auto-Aim to CAB',
      description: '敵のCABに自動的にエイムを合わせます',
      category: 'Combat',
      color: 'from-red-400 to-rose-600',
    },
    {
      icon: FaBolt,
      title: 'One-Hit Kill',
      description: '全ての敵を一撃で倒します',
      category: 'Combat',
      color: 'from-yellow-400 to-orange-600',
    },
    {
      icon: FaRocket,
      title: 'NoClip',
      description: '壁抜け - あらゆる障害物を通り抜けられます',
      category: 'Movement',
      color: 'from-blue-400 to-cyan-600',
    },
    {
      icon: FaMapMarkedAlt,
      title: 'Teleport System',
      description: '任意の座標へ瞬時にテレポート + カスタムウェイポイント',
      category: 'Movement',
      color: 'from-purple-400 to-violet-600',
    },
    {
      icon: FaCoins,
      title: 'Infinite Resources',
      description: '無限リソース - お金とブロックが無制限に',
      category: 'Resources',
      color: 'from-amber-400 to-yellow-600',
    },
    {
      icon: FaCube,
      title: 'Unlock All Blocks',
      description: '全てのブロックを即座にアンロック',
      category: 'Resources',
      color: 'from-teal-400 to-cyan-600',
    },
    {
      icon: FaEye,
      title: 'Enemy ESP',
      description: '敵CABを壁越しに可視化 + 距離表示',
      category: 'Visual',
      color: 'from-pink-400 to-fuchsia-600',
    },
    {
      icon: FaRobot,
      title: 'Disable Enemy AI',
      description: '敵AIを無効化 - 全ての敵が動かなくなります',
      category: 'Combat',
      color: 'from-indigo-400 to-blue-600',
    },
    {
      icon: FaFire,
      title: 'Fire Rate Multiplier',
      description: '武器の連射速度を最大10倍まで増加',
      category: 'Combat',
      color: 'from-orange-400 to-red-600',
    },
    {
      icon: FaClock,
      title: 'Time Control',
      description: 'ゲームスピードを0.1x～5.0xまで変更可能',
      category: 'Misc',
      color: 'from-slate-400 to-gray-600',
    },
    {
      icon: FaCog,
      title: 'Tech Editor',
      description: '敵Techを編集モードで改造可能',
      category: 'Tech',
      color: 'from-emerald-400 to-green-600',
    },
  ];

  // コンテナのアニメーション設定
  const containerVariants = {
    hidden: { opacity: 0 },
    visible: {
      opacity: 1,
      transition: {
        staggerChildren: 0.1,
      },
    },
  };

  // カードのアニメーション設定
  const cardVariants = {
    hidden: { opacity: 0, y: 20 },
    visible: {
      opacity: 1,
      y: 0,
      transition: {
        duration: 0.5,
      },
    },
  };

  return (
    <section id="features" className="relative py-24 px-6 overflow-hidden">
      {/* 背景デコレーション */}
      <div className="absolute inset-0 opacity-10">
        <div className="absolute top-20 left-10 w-72 h-72 bg-cyber-blue rounded-full filter blur-3xl" />
        <div className="absolute bottom-20 right-10 w-96 h-96 bg-cyber-purple rounded-full filter blur-3xl" />
      </div>

      <div className="relative z-10 container mx-auto max-w-7xl">
        {/* セクションヘッダー */}
        <motion.div
          initial={{ opacity: 0, y: 20 }}
          whileInView={{ opacity: 1, y: 0 }}
          viewport={{ once: true }}
          transition={{ duration: 0.6 }}
          className="text-center mb-16"
        >
          <h2 className="text-4xl md:text-5xl font-display font-bold mb-4">
            <span className="text-gradient">究極の機能</span>
          </h2>
          <p className="text-xl text-gray-300 max-w-2xl mx-auto">
            30以上の強力な機能を搭載。ゲーム体験を完全に変革します。
          </p>
        </motion.div>

        {/* 機能カードグリッド */}
        <motion.div
          variants={containerVariants}
          initial="hidden"
          whileInView="visible"
          viewport={{ once: true }}
          className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6"
        >
          {features.map((feature, index) => (
            <motion.div
              key={index}
              variants={cardVariants}
              whileHover={{ y: -10, scale: 1.02 }}
              className="group relative glass rounded-2xl p-6 cursor-pointer overflow-hidden"
            >
              {/* ホバー時のグラデーション背景 */}
              <div className={`absolute inset-0 bg-gradient-to-br ${feature.color} opacity-0 group-hover:opacity-10 transition-opacity duration-300`} />

              {/* カテゴリバッジ */}
              <div className="absolute top-4 right-4">
                <span className="text-xs px-3 py-1 bg-white bg-opacity-10 rounded-full">
                  {feature.category}
                </span>
              </div>

              {/* アイコン */}
              <div className={`relative inline-flex items-center justify-center w-16 h-16 mb-4 bg-gradient-to-br ${feature.color} rounded-xl`}>
                <feature.icon className="text-2xl text-white" />
              </div>

              {/* タイトル */}
              <h3 className="text-xl font-display font-bold mb-2 text-white group-hover:text-cyber-blue transition-colors duration-300">
                {feature.title}
              </h3>

              {/* 説明 */}
              <p className="text-gray-400 text-sm leading-relaxed">
                {feature.description}
              </p>

              {/* ホバー時の装飾 */}
              <div className={`absolute bottom-0 left-0 right-0 h-1 bg-gradient-to-r ${feature.color} transform scale-x-0 group-hover:scale-x-100 transition-transform duration-300`} />
            </motion.div>
          ))}
        </motion.div>

        {/* さらに多くの機能 */}
        <motion.div
          initial={{ opacity: 0, y: 20 }}
          whileInView={{ opacity: 1, y: 0 }}
          viewport={{ once: true }}
          transition={{ delay: 0.5, duration: 0.6 }}
          className="mt-12 text-center"
        >
          <p className="text-gray-400 mb-4">そして、さらに20以上の機能...</p>
          <div className="flex flex-wrap justify-center gap-3">
            {[
              'Auto Harvest',
              'Infinite Collector',
              'Expand Map Range',
              'Enhanced Storage',
              'Destroy CAB Only',
              'Custom FPS Display',
              'Position Display',
              'Time of Day Control',
            ].map((feat, index) => (
              <motion.span
                key={index}
                initial={{ opacity: 0, scale: 0.8 }}
                whileInView={{ opacity: 1, scale: 1 }}
                viewport={{ once: true }}
                transition={{ delay: index * 0.05, duration: 0.3 }}
                whileHover={{ scale: 1.1 }}
                className="px-4 py-2 glass rounded-full text-sm text-gray-300 hover:text-cyber-blue transition-colors duration-300"
              >
                {feat}
              </motion.span>
            ))}
          </div>
        </motion.div>
      </div>
    </section>
  );
};

export default Features;
