import { Link } from "react-router-dom";
import "../Styles/footer.css"
import FacebookIcon from "./FacebookIcon";
import InstagramIcon from "./InstagramIcon";
import LinkedInIcon from "./LinkedinIcon";

const Footer = () => {
  return (
    <div className="footer" data-testid="footer-id">
      <footer className="footer1" data-testid="footer-container">
        <div className="container6">
          <span className="logo2 mulish-font">SKILLSYNC</span>
          <nav className="nav1">
          
          <Link to="/"><span className="nav12">Home</span></Link>
          <Link to="/about"><span className="nav22">About</span></Link>
      
          
          </nav>
        </div>
        <div className="separator"></div>
        <div className="container7">
          <span className="text6 mulish-font">
            © 2023 SkillsSynkc <img src="src\assets\dans_logo_png.png" alt="SkillsSync Logo" className="footer-logo" />
          </span>

          <div className="contact-info">
            <p>Norrkoping, Sweden</p>
            <p></p>
            <p>Phone: 0140-44 45 10</p>
            <p></p>
            <p>Email: info@skillsync.com</p>
          </div>
          
          <div className="icon-group1">
            <InstagramIcon></InstagramIcon>
            <LinkedInIcon></LinkedInIcon>
            <FacebookIcon></FacebookIcon>
            </div>
          </div> 
      </footer>
    </div>
    /*<footer className="footer mt-auto py-3" data-testid="footer-id">
      <Container className="bg-light" data-testid="footer-container">
        <p>© {new Date().getFullYear()} SkillSync</p>
      </Container>
    </footer>*/
  );
};

export default Footer;
