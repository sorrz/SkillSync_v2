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

function App() {
  const [user, setUser] = useState<StudentModel | null>(null);
  const navigate = useNavigate();
  const handleLogin = (loggedInUser: StudentModel) => {
    setUser(loggedInUser);
  };

  const handleLogout = () => {
    navigate('/');
    setUser(null);
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
          </Routes>
        </Container>
        <Footer />
      </div>
    </>
  );
}

export default App;
