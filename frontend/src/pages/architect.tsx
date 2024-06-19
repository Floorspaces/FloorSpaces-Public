import styles from '../styles/architect.module.css';
import ArchitectHeader from './ribbons/architectHeader';
import Footer from './ribbons/footer';
import LeftNav from './core/leftNav';
import 'bootstrap/dist/css/bootstrap.min.css';
import "../styles/globals.css";

const Architect = () => {
  return (
    <div className={styles.container}>
      <LeftNav />
      <div className={styles.contentWrapper}>
        <ArchitectHeader />
        {/* Unity renders at 1665 x 790 */}
        <iframe 
          src="/architectBuild/index.html"
          className={styles.unityIframe}
          style={{width: '100%', height: '803px', border: 'none'}}
        ></iframe>
        <Footer />
      </div>
    </div>
  );
}

export default Architect;
