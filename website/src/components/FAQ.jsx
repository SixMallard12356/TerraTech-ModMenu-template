import { useState } from 'react';
import { motion, AnimatePresence } from 'framer-motion';
import { FaChevronDown, FaQuestionCircle } from 'react-icons/fa';

/**
 * FAQセクション - よくある質問
 * アコーディオン形式で質問と回答を表示
 */
const FAQ = () => {
  const [openIndex, setOpenIndex] = useState(null);

  // よくある質問リスト
  const faqs = [
    {
      question: 'このModは安全ですか？',
      answer: 'このModはオープンソースで、悪意のあるコードは含まれていません。ただし、DLLインジェクションという技術を使用しているため、一部のウイルス対策ソフトが誤検知する可能性があります。ソースコードはGitHubで公開されていますので、ご自身で確認することができます。',
    },
    {
      question: 'オンラインで使用できますか？',
      answer: 'いいえ、絶対に使用しないでください。オンラインプレイでの使用はゲームの利用規約に違反し、アカウントがBANされる可能性があります。このModはシングルプレイ専用です。',
    },
    {
      question: 'Injectorが失敗します。どうすればいいですか？',
      answer: '以下を確認してください：\n1. TerraTechが起動しているか\n2. 管理者権限で実行しているか\n3. ウイルス対策ソフトがブロックしていないか\n4. ModMenu.dllがInjector.exeと同じフォルダにあるか\n5. .NET Framework 4.7.2以上がインストールされているか',
    },
    {
      question: 'メニューが表示されません',
      answer: 'INSERTキーを押してメニューを開いてください。それでも表示されない場合は、以下を確認してください：\n1. Injectionが成功したか（コンソールに「Injection successful!」と表示される）\n2. ゲームのログファイルを確認（%APPDATA%\\..\\LocalLow\\Payload Studios\\TerraTech\\output_log.txt）\n3. ゲームを再起動してみる',
    },
    {
      question: '一部の機能が動作しません',
      answer: 'TerraTechのバージョンによっては、一部の機能が動作しない可能性があります。ゲームのアップデート後は、ModMenuも更新が必要な場合があります。GitHubのIssuesで報告していただければ、対応を検討します。',
    },
    {
      question: 'アンインストール方法は？',
      answer: 'ゲームを再起動するだけで、ModMenuは自動的にアンロードされます。完全に削除したい場合は、ダウンロードしたファイル（Injector.exe、ModMenu.dll）を削除してください。',
    },
    {
      question: 'セーブデータは安全ですか？',
      answer: 'ModMenuはゲームのセーブデータを直接変更しませんが、一部の機能（お金追加、ブロックアンロック等）を使用すると、セーブデータに影響が出ます。念のため、使用前にセーブデータのバックアップを推奨します。',
    },
    {
      question: '新しい機能のリクエストはできますか？',
      answer: 'はい！GitHubのIssuesで機能リクエストを受け付けています。実装できるかどうかは技術的な制約によりますが、できる限り対応したいと思います。',
    },
  ];

  // アコーディオンの開閉をトグル
  const toggleFAQ = (index) => {
    setOpenIndex(openIndex === index ? null : index);
  };

  return (
    <section id="faq" className="relative py-24 px-6">
      <div className="container mx-auto max-w-4xl">
        {/* セクションヘッダー */}
        <motion.div
          initial={{ opacity: 0, y: 20 }}
          whileInView={{ opacity: 1, y: 0 }}
          viewport={{ once: true }}
          transition={{ duration: 0.6 }}
          className="text-center mb-16"
        >
          <div className="inline-flex items-center justify-center w-16 h-16 bg-gradient-to-br from-cyber-blue to-cyber-purple rounded-full mb-6">
            <FaQuestionCircle className="text-3xl text-white" />
          </div>
          <h2 className="text-4xl md:text-5xl font-display font-bold mb-4">
            <span className="text-gradient">よくある質問</span>
          </h2>
          <p className="text-xl text-gray-300">
            疑問や問題を解決しましょう
          </p>
        </motion.div>

        {/* FAQ リスト */}
        <div className="space-y-4">
          {faqs.map((faq, index) => (
            <motion.div
              key={index}
              initial={{ opacity: 0, y: 20 }}
              whileInView={{ opacity: 1, y: 0 }}
              viewport={{ once: true }}
              transition={{ delay: index * 0.05, duration: 0.4 }}
            >
              <div className="glass rounded-xl overflow-hidden">
                {/* 質問部分（クリック可能） */}
                <button
                  onClick={() => toggleFAQ(index)}
                  className="w-full px-6 py-5 flex items-center justify-between text-left hover:bg-white hover:bg-opacity-5 transition-colors duration-300"
                >
                  <span className="text-lg font-bold pr-4">{faq.question}</span>
                  <motion.div
                    animate={{ rotate: openIndex === index ? 180 : 0 }}
                    transition={{ duration: 0.3 }}
                    className="flex-shrink-0"
                  >
                    <FaChevronDown className="text-cyber-blue" />
                  </motion.div>
                </button>

                {/* 回答部分（アコーディオン） */}
                <AnimatePresence>
                  {openIndex === index && (
                    <motion.div
                      initial={{ height: 0, opacity: 0 }}
                      animate={{ height: 'auto', opacity: 1 }}
                      exit={{ height: 0, opacity: 0 }}
                      transition={{ duration: 0.3 }}
                      className="overflow-hidden"
                    >
                      <div className="px-6 pb-5 pt-2">
                        <div className="pl-4 border-l-2 border-cyber-blue">
                          <p className="text-gray-300 whitespace-pre-line leading-relaxed">
                            {faq.answer}
                          </p>
                        </div>
                      </div>
                    </motion.div>
                  )}
                </AnimatePresence>
              </div>
            </motion.div>
          ))}
        </div>

        {/* サポート情報 */}
        <motion.div
          initial={{ opacity: 0, y: 20 }}
          whileInView={{ opacity: 1, y: 0 }}
          viewport={{ once: true }}
          transition={{ delay: 0.5, duration: 0.6 }}
          className="mt-12 text-center"
        >
          <div className="glass rounded-2xl p-8">
            <h3 className="text-2xl font-display font-bold mb-4">
              まだ解決しませんか？
            </h3>
            <p className="text-gray-300 mb-6">
              GitHubのIssuesで質問やバグ報告を受け付けています
            </p>
            <motion.a
              whileHover={{ scale: 1.05 }}
              whileTap={{ scale: 0.95 }}
              href="https://github.com/SixMallard12356/TerraTech-ModMenu/issues"
              target="_blank"
              rel="noopener noreferrer"
              className="inline-block px-8 py-3 bg-gradient-to-r from-cyber-blue to-cyber-purple rounded-full font-bold hover:glow-blue transition-all duration-300"
            >
              質問する
            </motion.a>
          </div>
        </motion.div>
      </div>
    </section>
  );
};

export default FAQ;
