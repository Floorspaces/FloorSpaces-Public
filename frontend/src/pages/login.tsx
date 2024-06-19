import { useState, FormEvent } from 'react';
import Image from "next/image";
import { useRouter } from 'next/router'; // Import useRouter hook
import 'bootstrap/dist/css/bootstrap.min.css';
import styles from '../styles/login.module.css';
import PortalHeader from "../pages/ribbons/portalHeader";
import Footer from "../pages/ribbons/footer";

const Login = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const router = useRouter(); // Initialize useRouter hook

  const isProduction = process.env.NODE_ENV === 'production';

  const apiUrl = isProduction
    ? 'https://floorspaces.azurewebsites.net/api/auth/'
    : 'https://localhost:5269/api/auth/';
  
  const handleLogin = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    
    try {
      const response = await fetch(apiUrl + 'Login', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          email: username,
          password: password,
        }),
      });
  
      if (response.ok) {
        const data = await response.json();
        console.log('Login successful:', data);
      
        // Store the access token in localStorage
        localStorage.setItem('accessToken', data.access_token);
      
        // Redirect to dashboard page
        router.push('/core/dashboard');
      } else {
        // Handle failed login
        console.error('Login failed:', response.statusText);
      }
    } catch (error) {
      console.error('Error during login:', error);
    }
  };
  

  return (
    <div className={`${styles.loginPage} vh-100 d-flex flex-column `}>
      <PortalHeader />
      <div className={`${styles.loginContainer} d-flex justify-content-center align-items-center flex-grow-1`}>
        <div className={`${styles.loginForm} rounded shadow p-4 d-flex flex-column justify-content-start align-items-center`}>
          <div className="row g-0 flex-grow-1">
            <div className="col-md-6">
              <h2 className="mt-4 mb-4 ms-2 display-6 text-light" style={{ fontSize: '4.5rem' }}>Welcome</h2>
              <p style={{ fontSize: '1.4rem' }} className={`${styles.loginMessage} mb-2 ms-2 opacity-50`}>
                Login to your account to access <b>Floorspaces:</b> for architects, administrators, stakeholders & employees alike.
              </p>
              <div className="p-4">
                <form className="w-100" onSubmit={handleLogin}>
                  <div className="mb-3">
                    <label htmlFor="username" style={{ fontSize: '1.2rem' }} className="form-label text-light">
                      Username:
                    </label>
                    <input 
                      className={`${styles.loginFormBox} form-control text-light`}
                      type="text"
                      id="username"
                      value={username}
                      onChange={(e) => setUsername(e.target.value)}
                      placeholder="Enter your username"
                    />
                  </div>
                  <div className="mb-4">
                    <label htmlFor="password" style={{ fontSize: '1.2rem' }} className="form-label text-light">
                      Password:
                    </label>
                    <input
                      type="password"
                      className={`${styles.loginFormBox} form-control text-light`}
                      id="password"
                      value={password}
                      onChange={(e) => setPassword(e.target.value)}
                      placeholder="Enter your password"
                    />
                  </div>
                  <div className="mt-2 d-flex justify-content-end">
                    <button type="submit" className={`${styles.customButton} btn btn-primary btn-lg`}>
                      Login
                    </button>
                  </div>
                </form>
              </div>
            </div>

            <div className="col-md-6 d-flex justify-content-center align-items-center">
              <div className="w-100 h-100 position-relative">
              <Image
                src="/LoginPicture.png"
                alt="Web App Icon"
                layout="fill"
                objectFit="cover"
                objectPosition="center"
                sizes="(max-width: 768px) 100vw, (max-width: 1200px) 50vw, 33vw"
                className={`${styles.loginImage} rounded-start`}
              />
              </div>
            </div>
          </div>
        </div>
      </div>
      <Footer />
    </div>
  );
};

export default Login;
