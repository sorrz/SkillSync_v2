import { Container, Nav, Navbar as NavbarBS } from "react-bootstrap";
import { NavLink } from "react-router-dom";
import { Button } from "antd";
import { UserModel } from "../Models/UserModel";


interface NavbarProps {
    isLoggedIn: boolean;
    user: UserModel;
}

export function Navbar({ isLoggedIn, user }: NavbarProps) {
    const imgSrc = user?.imgURL || "https://www.svgbackgrounds.com/wp-content/uploads/2021/07/slanted-halftone-subtle-background-graphic.jpg"; // Replace "solid-color-url" with your actual URL or color

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
                    <Nav.Link to="/profile" as={NavLink}>
                        <img src={imgSrc} alt="Profile" className="profile-picture" />
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
