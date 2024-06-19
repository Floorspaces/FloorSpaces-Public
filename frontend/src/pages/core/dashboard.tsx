import { useState, useEffect } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import styles from '../../styles/core/dashboard.module.css'; // Create a CSS file for dashboard styles
import LeftNav from './leftNav';
import PortalHeader from '../ribbons/portalHeader';

interface Profile {
  user_id: string;
  firstname?: string | null;
  lastname?: string | null;
  occupation?: string | null;
  status?: string | null;
  pronouns?: string | null;
  bio?: string | null;
  birthday?: Date | null;
}

interface Props {
  accessToken: string;
}

const Dashboard: React.FC<Props> = ({ accessToken }) => {
  const [profile, setProfile] = useState<Profile | null>(null);

  useEffect(() => {
    const apiUrl = process.env.NODE_ENV === 'production'
      ? 'https://floorspaces.azurewebsites.net/api/auth/'
      : 'https://localhost:5269/api/auth/';
  
    const accessToken = localStorage.getItem('accessToken');
  
    const fetchProfile = async () => {
      if (!accessToken) {
        console.error('No access token available.');
        return;
      }
      
      console.log(`Bearer ${accessToken}`);

      try {
        console.log('Access Token from Local Storage:', accessToken);

        const response = await fetch(apiUrl + 'Profile', {
          method: 'GET',
          headers: {
            'Authorization': `Bearer ${accessToken}`, 
            'Content-Type': 'application/json',
          },
        })
        
  
        if (response.ok) {
          const data = await response.json();
          setProfile(data);
        } else {
          console.error('Failed to fetch profile:', response.statusText);
          // Handle logout or token refresh if necessary
        }
      } catch (error) {
        console.error('Error fetching profile:', error);
      }
    };
  
    fetchProfile();
  }, []);
  

  return (
    <div className="vw-100 vh-100">
      <div className={`${styles.dashboardContainer} d-flex vh-100`}>
        <LeftNav />
        <div className={`${styles.dashboardContent} rounded shadow-lg p-4 d-flex flex-column justify-content-center align-items-center`} style={{ flexGrow: 1, maxWidth: '80%' }}>

          <h2 className="mb-4 display-1 text-light">Dashboard</h2>
          {profile && (
            <table className="table table-dark">
              <tbody>
                <tr className="bg-secondary">
                  <td>Pronouns</td>
                  <td>{profile.pronouns}</td>
                </tr>
                <tr className="bg-dark">
                  <td>Personal Bio (140 characters or less):</td>
                  <td>{profile.bio}</td>
                </tr>
                <tr className="bg-secondary">
                  <td>First Name</td>
                  <td>{profile.firstname}</td>
                </tr>
                <tr className="bg-dark">
                  <td>Last Name</td>
                  <td>{profile.lastname}</td>
                </tr>
                <tr className="bg-secondary">
                  <td>Occupation</td>
                  <td>{profile.occupation}</td>
                </tr>
                <tr className="bg-dark">
                  <td>Status</td>
                  <td>{profile.status}</td>
                </tr>
                <tr className="bg-secondary">
                  <td>Date of Birth (DOB):</td>
                  <td>{profile.birthday ? new Date(profile.birthday).toLocaleDateString() : 'N/A'}</td>
                </tr>
              </tbody>
            </table>
          )}
        </div>
      </div>
    </div>
  );
};

export default Dashboard;
