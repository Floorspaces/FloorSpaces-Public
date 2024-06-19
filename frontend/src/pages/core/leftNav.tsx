import Link from "next/link";
import Image from "next/image";

const LeftNav = () => {
  return (
    <div className="d-flex flex-column vh-100" style={{width: '12%', border: '2px solid gray', backgroundColor: 'rgb(20, 15, 15)'}}>
      <div className="flex-grow-1 d-flex flex-column justify-content-between">
        {/* Navigation Buttons */}
        <div>
          <Link href="/core/dashboard" className="btn btn-outline-secondary btn-lg w-100 d-flex align-items-center justify-content-center flex-column square-btn">
            <Image src="/nav-icons/profiles-icon.png" alt="Profiles Icon" width={169} height={169} />
            <span style={{fontSize: '2rem'}}>My Profile</span>
          </Link>
          <Link href="/architect" className="btn btn-outline-secondary btn-lg w-100 d-flex align-items-center justify-content-center flex-column square-btn">
            <Image src="/nav-icons/architect-icon.png" alt="Architect Icon" width={169} height={169} />
            <span style={{fontSize: '2rem'}}>Architect</span>
          </Link>
          <Link href="/floorplans" className="btn btn-outline-secondary btn-lg w-100 d-flex align-items-center justify-content-center flex-column square-btn">
            <Image src="/nav-icons/app-icon.png" alt="Web App Icon" width={169} height={169} />
            <span style={{fontSize: '2rem'}}>Floorspace</span>
          </Link>
          <Link href="/core/admin" className="btn btn-outline-secondary btn-lg w-100 d-flex align-items-center justify-content-center flex-column square-btn">
            <Image src="/nav-icons/management-icon.png" alt="Web App Icon" width={169} height={169} />
            <span style={{fontSize: '2rem'}}>Management</span>
          </Link>
        </div>
      </div>

      {/* Additional space or elements can be added here if needed */}
    </div>
  );
};

export default LeftNav;
