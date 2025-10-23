import Navbar from './components/Navbar';
import Hero from './components/Hero';
import Features from './components/Features';
import Installation from './components/Installation';
import Gallery from './components/Gallery';
import Download from './components/Download';
import FAQ from './components/FAQ';
import Footer from './components/Footer';

/**
 * メインアプリケーションコンポーネント
 * 全てのセクションを統合
 */
function App() {
  return (
    <div className="relative overflow-x-hidden">
      <Navbar />

      <main>
        <Hero />
        <Features />
        <Installation />
        <Gallery />
        <Download />
        <FAQ />
      </main>

      <Footer />
    </div>
  );
}

export default App;
