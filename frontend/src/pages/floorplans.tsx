// @ts-ignore
import styles from '../styles/floorplans.module.css';
import LeftNav from './core/leftNav'
import Footer from './ribbons/footer';
import FloorplansHeader from './ribbons/floorplansHeader';
import 'bootstrap/dist/css/bootstrap.min.css';
import "../styles/globals.css";

const Floorplans = () => {
  return (
    <div className={styles.container}>
      <LeftNav />
      <div className={styles.contentWrapper}>
        <FloorplansHeader />
        {/* Unity renders at 1665 x 790 */}
        <iframe 
          src="/viewerBuild/index.html"
          className={styles.unityIframe}
          style={{width: '100%', height: '803px', border: 'none'}}
        ></iframe>
        <Footer />
      </div>
    </div>
  );
}

export default Floorplans;
