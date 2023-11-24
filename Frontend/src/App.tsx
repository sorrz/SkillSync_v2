import { Routes, Route } from "react-router-dom";
import { Container } from "react-bootstrap";
import { About } from "./Pages/About";
import { Companies } from "./Pages/Companies";
import { Home } from "./Pages/Home";
import { Students } from "./Pages/Students";
import { Navbar } from "./Components/Navbar";
import { Login } from "./Pages/Login";

function App() {
 

  return (
    <>
    <Navbar isLoggedIn={false} userProfile={{ picture: 'path/to/profile-picture.jpg' }} />
    <Container className="mb-4">
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/companies" element={<Companies />} />
        <Route path="/students" element={<Students />} />
        <Route path="/about" element={<About />} />
        <Route path="/login" element={<Login />} />
      </Routes> 
    </Container>
    </>
  )
}

export default App
