import Image from "next/image";
import Link from "next/link";
import ProductHeader from "../pages/ribbons/productHeader";
import Button from 'react-bootstrap/Button';

export default function Home() {
  return (
    <div className="d-flex flex-column min-vh-100 overflow-hidden">
      <ProductHeader />
      {/* Main Content */}
      <main className="d-flex flex-grow-1 justify-content-center align-items-center product-background">
        <div className="container">
          <div className="left-column">
            <div className="left-column-content">
              <h1 className="display-2 ibm-plex-sans-bold title-beige-text">floorspaces.io</h1>
              <p className="py-4 display-6 ibm-plex-sans-regular">
                Graphics software to embed business applications inside
                <span className="title-beige-text"> interactive 3D maps</span> of your business venue.
              </p>
              <div className="mb-4">
                <Link href="/portal">
                  <Button variant="primary" className="w-30 product-button product-beige-button ibm-plex-sans-medium gray-button-hover me-2">
                    Get started!
                  </Button>
                </Link>
                <Link href="/brochure">
                  <Button variant="primary" className="product-button product-white-button ibm-plex-sans-medium gray-button-hover">
                    What is this?
                  </Button>
                </Link>
              </div>
            </div>
          </div>
          <div className="right-column">
            <div className="right-column-content">
              {/* Video Section */}
              <div style={{ margin: "20px" }}>
                <video autoPlay loop muted style={{ maxWidth: "100%" }}>
                  <source src="/demo.mp4" type="video/mp4" />
                  Your browser does not support the video tag.
                </video>
              </div>
            </div>
          </div>
        </div>
      </main>
    </div>
  );
}
