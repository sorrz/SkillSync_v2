import { useState } from 'react';
import { Routes, Route, useNavigate } from 'react-router-dom';
import { Container } from 'react-bootstrap';
import { About } from './Pages/About';
import { Companies } from './Pages/Companies';
import { Home } from './Pages/Home';
import { Students } from './Pages/Students';
import { Navbar } from './Components/Navbar';
import { Login } from './Pages/Login';
import Footer from './Components/Footer';
import StudentModel from './Models/StudentModel';
import { Profile } from './Pages/Profile';
import  Register  from './Pages/Register';
import { StudentLogin } from './Services/CompanyServices';

function App() { 

  const [user, setUser] = useState<StudentModel | null>(null);
  const navigate = useNavigate();

  async function doHash(password: string) {
  const encoder = new TextEncoder();
  const data = encoder.encode(password);

  const hashBuffer = await crypto.subtle.digest('SHA-256', data);

  const hashArray = Array.from(new Uint8Array(hashBuffer));
  const hashHex = hashArray.map(byte => byte.toString(16).padStart(2, '0')).join('');

  return hashHex;
}

  const handleLogout = () => {
    navigate('/home');
    setUser(null);
  };

  const handleLogin = async (email: string, password: string) => {
    const hashedPassword = await doHash(password);

    try {
      const loggedInUser = await StudentLogin(email, hashedPassword);
      setUser(loggedInUser);
      console.log(loggedInUser);
      navigate('/profile');
    } catch (error) {
      console.error('Login failed', error);
    }
  };

  const handleRegistration = (student: StudentModel) => {
    // Handle the registration logic (e.g., send data to the server)
    console.log('Registered student:', student);
  };

  return (
    <>
      <div style={{ display: 'flex', flexDirection: 'column', minHeight: '100vh' }}>
        <Navbar isLoggedIn={!!user} user={user} onLogout={handleLogout} />
        <Container className="mb-4" style={{ flex: 1 }}>
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/companies" element={<Companies />} />
            <Route path="/students" element={<Students />} />
            <Route path="/about" element={<About />} />
            <Route
              path="/login"
              element={<Login onLogin={handleLogin} />}
            />
            <Route path="/profile" element={<Profile />} />
            <Route path="/register" element={<Register handleRegistration={handleRegistration} />} />
          </Routes>
        </Container>
        <Footer />
      </div>
    </>
  );
}

export default App;
