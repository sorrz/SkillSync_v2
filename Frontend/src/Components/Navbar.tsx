import { Container, Nav, Navbar as NavbarBS } from "react-bootstrap";
import { NavLink } from "react-router-dom";
import { Button } from "antd";

interface UserProfile {
    picture: string;
    // Add other properties as needed
}

interface NavbarProps {
    isLoggedIn: boolean;
    userProfile?: UserProfile;
}

export function Navbar({ isLoggedIn, userProfile }: NavbarProps) {
    return (
        <NavbarBS sticky="top" className="bg-white shadow-sm mb-3">
            <Container>
                <Nav className="me-auto">
                    <Nav.Link to="/" as={NavLink} > Home </Nav.Link>
                    <Nav.Link to="/companies" as={NavLink} > Companies </Nav.Link>
                    <Nav.Link to="/students" as={NavLink} > Students </Nav.Link>
                    <Nav.Link to="/about" as={NavLink} > About </Nav.Link>
                </Nav>
                {isLoggedIn ? (
                    // Render user profile picture and link if logged in
                    <Nav.Link to="/profile" as={NavLink}>
                        <img src={userProfile?.picture} alt="Profile" className="profile-picture" />
                    </Nav.Link>
                ) : (
                    <Nav.Link to="/login" as={NavLink}>
                    <Button>Login</Button>
                    </Nav.Link>
                )}
            </Container>
        </NavbarBS>
    );
}
