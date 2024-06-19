import Image from "next/image";
import Link from "next/link";
import PortalHeader from "../pages/ribbons/portalHeader";
import Footer from "../pages/ribbons/footer";
import 'bootstrap/dist/css/bootstrap.min.css';
import "../styles/globals.css";

const Portal = () => {
  return (
    <div className="d-flex flex-column min-vh-100 overflow-hidden">
      
      <PortalHeader />
      
      {/* Main Content */}
      <main className="d-grid gap-0 flex-grow-1 grid-container">
        {/* Your existing content */}
        {/* Row 1 */}
        {/* Login Nav Button */}
        <Link href="/login" className="btn btn-outline-secondary btn-lg square-btn">
          <Image
            src="/nav-icons/login-icon.png"
            alt="Login Icon"
            width={160}
            height={160}
            className="mb-4"
          />
          <h2 className="mb-3 display-6 ibm-plex-sans-medium">Account Login</h2>
          <p className="m-0 text-md opacity-50 nav-description ibm-plex-sans-regular">
            Log into your account to access the floorspaces.io platform in production.
          </p>
        </Link>

        {/* Floorspace Architect Sandbox Nav Button */}
        <Link href="/architect" className="btn btn-outline-secondary btn-lg square-btn">
          <Image
            src="/nav-icons/architect-icon.png"
            alt="Architect Icon"
            width={160}
            height={160}
            className="mb-4"
          />
          <h2 className="mb-3 display-6 ibm-plex-sans-medium title-beige-text">Architect</h2>
          <p className="m-0 text-md opacity-50 nav-description ibm-plex-sans-regular">Prototype our floorplan designer Unity project to try building a floorspace.</p>
        </Link>

        {/* Floorspace View Sandbox Nav Button */}
        <Link href="/floorplans" className="btn btn-outline-secondary btn-lg square-btn">
          <Image
            src="/nav-icons/app-icon.png"
            alt="Web App Icon"
            width={160}
            height={160}
            className="mb-4"
          />
          <h2 className="mb-3 display-6 ibm-plex-sans-medium title-cyan-text">Floorspaces App</h2>
          <p className="m-0 text-md opacity-50 nav-description ibm-plex-sans-regular">Prototype our Unity project to render and interact with company floorplans.</p>
        </Link>

        {/* Row 2 */}
        {/* Return Home Button */}
        <Link href="/" className="btn btn-outline-secondary btn-lg square-btn">
          <Image
            src="/nav-icons/home-icon.png"
            alt="Home Icon"
            width={160}
            height={160}
            className="mb-4"
          />
          <h2 className="mb-3 display-6 ibm-plex-sans-medium">Return Home</h2>
          <p className="m-0 text-md opacity-50 nav-description ibm-plex-sans-regular">
            Use this button to return to the main Floorspaces.io public landing page.
          </p>
        </Link>

        {/* Commercial Advertisement Nav Button */}
        <Link href="/brochure" className="btn btn-outline-secondary btn-lg square-btn">
          <Image
            src="/nav-icons/factory-icon.png"
            alt="Factory Icon | Business"
            width={160}
            height={160}
            className="mb-4"
          />
          <h2 className="mb-3 display-6 ibm-plex-sans-medium">For <span className="font-bold">YOUR</span> Business</h2>
          <p className="m-0 text-md opacity-50 nav-description ibm-plex-sans-regular">
            Find out how we integrate easily, and the next steps to live maps for your business!
          </p>
        </Link>

        {/* Contact Us Nav Button */}
        <a
          href="mailto:floorspaces@gmail.com"
          className="btn btn-outline-secondary btn-lg square-btn"
          target="_blank"
          rel="noopener noreferrer"
        >
          <Image
            src="/nav-icons/chat-icon.png"
            alt="Contact Us Icon"
            width={160}
            height={160}
            className="mb-4"
          />
          <h2 className="mb-3 display-6 ibm-plex-sans-medium">Contact Our Team</h2>
          <p className="m-0 text-md opacity-50 nav-description ibm-plex-sans-regular">Message us to test out the system for yourself, we&apos;ll bring Floorspaces to you!</p>
        </a>
      </main>

      <Footer />
    </div>
  );
}

export default Portal;