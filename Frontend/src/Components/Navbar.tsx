import { Container, Nav, Navbar as NavbarBS } from 'react-bootstrap';
import { NavLink } from 'react-router-dom';
import { Button } from 'antd';
import { LogoutOutlined } from '@ant-design/icons'; // Import the Ant Design logout icon
import { UserModel } from '../Models/UserModel';
import "../Styles/navbar.css";
import logo from '../assets/dans_logo_png.png';
import profile from '../assets/profil.png';
import { useNavigate } from "react-router-dom";

interface NavbarProps {
  isLoggedIn: boolean;
  user: UserModel;
  onLogout: () => void;
}

export function Navbar({ isLoggedIn, user, onLogout }: NavbarProps) {
  
  const imgSrc = user?.imgURL || profile;
  const navigate = useNavigate();
  const doLogOut = () => {
    onLogout();
    navigate('/')
  };

  return (
    <NavbarBS sticky="top" className="bg-white shadow-sm mb-3">
      <Container>
        <div className="NavLinkContainer mx-1">
          <img style={{ width: "3rem", height: "3rem"}} src={logo} alt="SkillSync" />
        </div>
        <Nav className="me-auto">
          <Nav.Link to="/" as={NavLink} className="NavLinkContainer">
          <span className="NavLinkText">Home</span>
            <div className="NavLinkBackground"></div>
          </Nav.Link>
          <Nav.Link to="/companies" as={NavLink} className="NavLinkContainer">
          <span className="NavLinkText">Companies</span>
            <div className="NavLinkBackground"></div>
          </Nav.Link>
          <Nav.Link to="/students" as={NavLink} className="NavLinkContainer">
          <span className="NavLinkText">Students</span>
            <div className="NavLinkBackground"></div>
          </Nav.Link>
          <Nav.Link to="/about" as={NavLink} className="NavLinkContainer">
          <span className="NavLinkText">About</span>
            <div className="NavLinkBackground"></div>
          </Nav.Link>
        </Nav>
        {isLoggedIn ? (
          <>
            <Nav.Link to="/profile" as={NavLink}>
              <div style={{ position: "relative"}}>
              <img src={imgSrc} style={{ height: '3rem', width: '3rem', borderRadius: '50%', objectFit: 'cover' }} alt="Profile" className="profile-picture" /> 
              <Button className="logOutCircle rounded-circle d-flex justify-content-center align-items-center" 
                style={{  
                width: "2rem", 
                height: "2rem", 
                position: "absolute", 
                bottom: "30%", 
                right: "-30%", 
                transform: "translate(25%, 25%)",
              }} onClick={doLogOut} icon={<LogoutOutlined />} />
            </div>
            </Nav.Link>
          </> 
        ) : (
          <Nav.Link to="/login" as={NavLink}>
            <Button>Login</Button>
          </Nav.Link>
        )}
      </Container>
    </NavbarBS>
  );
}
