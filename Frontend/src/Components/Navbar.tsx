import { Container, Nav, Navbar as NavbarBS } from 'react-bootstrap';
import { NavLink } from 'react-router-dom';
import { Button } from 'antd';
import { LogoutOutlined } from '@ant-design/icons'; // Import the Ant Design logout icon
import StudentModel from '../Models/StudentModel';
import "../Styles/navbar.css";
import logo from '../assets/dans_logo_png.png';
import { useNavigate } from "react-router-dom";

interface NavbarProps {
  isLoggedIn: boolean;
  user: StudentModel;
  onLogout: () => void;
}

export function Navbar({ isLoggedIn, user, onLogout }: NavbarProps) {
  const navigate = useNavigate();

  const doLogOut = () => {
    onLogout();
    navigate('/')
  };

  const renderProfileContent = () => {
    if (isLoggedIn) {
      if (typeof user.imageUrl === "string") 
      {
        return (
          <>
            <img
              src={user.imageUrl}
              style={{ height: '3rem', width: '3rem', borderRadius: '50%', objectFit: 'cover' }}
              alt="Profile"
              data-testid="profile-id"
              className="profile-picture"
            />
            <Button
              className="logOutCircle rounded-circle d-flex justify-content-center align-items-center"
              style={{
                width: '2rem',
                height: '2rem',
                position: 'absolute',
                bottom: '30%',
                right: '-30%',
                transform: 'translate(25%, 25%)',
              }}
              onClick={doLogOut}
              icon={<LogoutOutlined />}
              data-testid="actionButton"
            />
          </>
        );
      } else {
        return (
          <div data-testid="profile-id"
            style={{
              height: '3rem',
              width: '3rem',
              borderRadius: '50%',
              backgroundColor: "orange",
            }}
          ><Button
          className="logOutCircle rounded-circle d-flex justify-content-center align-items-center"
          style={{
            width: '2rem',
            height: '2rem',
            position: 'absolute',
            bottom: '30%',
            right: '-30%',
            transform: 'translate(25%, 25%)',
          }}
          onClick={doLogOut}
          icon={<LogoutOutlined />}
          data-testid="actionButton"
        />
        </div>
        );
      }
    }
    return null;
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
          <Nav.Link to="/profile" as={NavLink}>
            <div style={{ position: 'relative' }}>{renderProfileContent()}</div>
          </Nav.Link>
        ) : (
          <Nav.Link to="/login" as={NavLink}>
            <Button data-testid="actionButton">Login</Button>
          </Nav.Link>
        )}
      </Container>
    </NavbarBS>
  );
}
