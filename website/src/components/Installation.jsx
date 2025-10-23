import { motion } from 'framer-motion';
import { FaPlay, FaDownload, FaKeyboard, FaCheckCircle } from 'react-icons/fa';

/**
 * Installationセクション - インストール手順
 * 3ステップの簡単なインストール方法を表示
 */
const Installation = () => {
  // インストール手順
  const steps = [
    {
      icon: FaPlay,
      number: '01',
      title: 'TerraTechを起動',
      description: '通常通りTerraTechを起動してゲームを開始します。',
      color: 'from-blue-400 to-cyan-600',
    },
    {
      icon: FaDownload,
      title: 'Injectorを実行',
      number: '02',
      description: 'ダウンロードしたInjector.exeを管理者権限で実行します。成功すると「Injection successful!」と表示されます。',
      color: 'from-purple-400 to-violet-600',
    },
    {
      icon: FaKeyboard,
      number: '03',
      title: 'INSERTキーで起動',
      description: 'ゲーム内でINSERTキーを押すとModMenuが表示されます。お好みの機能を有効化してお楽しみください！',
      color: 'from-pink-400 to-fuchsia-600',
    },
  ];

  // システム要件
  const requirements = [
    { label: 'OS', value: 'Windows 10/11 (64bit)' },
    { label: 'Game', value: 'TerraTech (Steam版)' },
    { label: 'Framework', value: '.NET Framework 4.7.2+' },
    { label: 'Permissions', value: '管理者権限' },
  ];

  return (
    <section id="installation" className="relative py-24 px-6 bg-gradient-to-b from-cyber-dark to-cyber-darker">
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
            <span className="text-gradient-orange">簡単インストール</span>
          </h2>
          <p className="text-xl text-gray-300 max-w-2xl mx-auto">
            たった3ステップで、すぐに使い始められます
          </p>
        </motion.div>

        {/* インストール手順 */}
        <div className="grid grid-cols-1 lg:grid-cols-3 gap-8 mb-16">
          {steps.map((step, index) => (
            <motion.div
              key={index}
              initial={{ opacity: 0, y: 30 }}
              whileInView={{ opacity: 1, y: 0 }}
              viewport={{ once: true }}
              transition={{ delay: index * 0.2, duration: 0.6 }}
              whileHover={{ y: -10 }}
              className="relative"
            >
              {/* ステップ番号（背景） */}
              <div className="absolute -top-6 -left-4 text-9xl font-display font-bold text-white opacity-5">
                {step.number}
              </div>

              {/* カードコンテンツ */}
              <div className="relative glass rounded-2xl p-8 h-full">
                {/* アイコン */}
                <div className={`inline-flex items-center justify-center w-16 h-16 mb-6 bg-gradient-to-br ${step.color} rounded-xl`}>
                  <step.icon className="text-2xl text-white" />
                </div>

                {/* タイトル */}
                <h3 className="text-2xl font-display font-bold mb-4 text-white">
                  {step.title}
                </h3>

                {/* 説明 */}
                <p className="text-gray-400 leading-relaxed">
                  {step.description}
                </p>

                {/* ステップ番号（小） */}
                <div className={`absolute top-6 right-6 w-10 h-10 bg-gradient-to-br ${step.color} rounded-full flex items-center justify-center font-bold text-sm`}>
                  {step.number}
                </div>
              </div>

              {/* 矢印（最後のカード以外） */}
              {index < steps.length - 1 && (
                <div className="hidden lg:block absolute top-1/2 -right-12 transform -translate-y-1/2 text-4xl text-cyber-blue opacity-30">
                  →
                </div>
              )}
            </motion.div>
          ))}
        </div>

        {/* システム要件 */}
        <motion.div
          initial={{ opacity: 0, y: 20 }}
          whileInView={{ opacity: 1, y: 0 }}
          viewport={{ once: true }}
          transition={{ duration: 0.6 }}
          className="max-w-4xl mx-auto"
        >
          <div className="glass rounded-2xl p-8">
            <h3 className="text-2xl font-display font-bold mb-6 flex items-center">
              <FaCheckCircle className="text-cyber-blue mr-3" />
              システム要件
            </h3>

            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
              {requirements.map((req, index) => (
                <motion.div
                  key={index}
                  initial={{ opacity: 0, x: -20 }}
                  whileInView={{ opacity: 1, x: 0 }}
                  viewport={{ once: true }}
                  transition={{ delay: index * 0.1, duration: 0.4 }}
                  className="flex items-center space-x-4 p-4 bg-white bg-opacity-5 rounded-lg"
                >
                  <div className="w-2 h-2 bg-cyber-blue rounded-full" />
                  <div>
                    <span className="text-gray-400 text-sm">{req.label}:</span>
                    <span className="ml-2 text-white font-medium">{req.value}</span>
                  </div>
                </motion.div>
              ))}
            </div>
          </div>
        </motion.div>

        {/* 注意事項 */}
        <motion.div
          initial={{ opacity: 0, y: 20 }}
          whileInView={{ opacity: 1, y: 0 }}
          viewport={{ once: true }}
          transition={{ delay: 0.3, duration: 0.6 }}
          className="mt-8 max-w-4xl mx-auto"
        >
          <div className="bg-gradient-to-r from-orange-500/20 to-red-500/20 border border-orange-500/30 rounded-xl p-6">
            <h4 className="text-lg font-bold text-orange-400 mb-2 flex items-center">
              ⚠️ 重要な注意事項
            </h4>
            <ul className="text-gray-300 space-y-2 text-sm">
              <li>• このModは教育目的で作成されています</li>
              <li>• オンラインプレイでの使用は禁止です（BANされる可能性があります）</li>
              <li>• セーブデータのバックアップを推奨します</li>
              <li>• ウイルス対策ソフトがブロックする場合は、除外リストに追加してください</li>
            </ul>
          </div>
        </motion.div>
      </div>
    </section>
  );
};

export default Installation;
