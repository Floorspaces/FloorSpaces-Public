// ProductHeader.tsx
import React from "react";
import Link from "next/link";
import Image from "next/image";
import styles from '../../styles/ribbons/productHeader.module.css'; 

const PortalHeader: React.FC = () => {
  return (
    <header className={`${styles.footerBg} mt-auto py-3`}> 
    <div className="container-fluid d-flex justify-content-start align-items-center">
        <div className="me-auto">
        <Link href="/" >
          <Image
            src="/TopLeftLogo.png"
            alt="Floorspaces Logo"
            width="225"
            height="30"
          />
        </Link>
        </div>
        <div className="flex-grow-1"></div>
        <div className={`${styles.footerText} ibm-plex-sans-medium me-3`}> 
        <small>Created by <span className="title-beige-text">David Reese</span> &
           <span className="title-cyan-text"> Nicholas Limbach</span></small>
        </div>
      </div>
    </header>
  );
};

export default PortalHeader;
