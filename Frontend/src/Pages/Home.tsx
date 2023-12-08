import { Typography } from "antd";
import { Container } from "react-bootstrap";
import  Carousel from "../Components/Carousel"
import "../Styles/homes.css";
import heroBannerImage from "../assets/herobanner.jpg"; // Replace with the actual path to your image

const { Title, Paragraph } = Typography;

export function Home() {
  return (
    <>
      <div className="hero-banner" style={{ height: "50vh", backgroundImage: `url(${heroBannerImage})` }}>
      <div className="overlay"></div>
        <Container>
          <Title style={{ color: "black" }} level={1}>SkillSync</Title>
          <Paragraph  className="lead" style={{ color: "black"}}> <b>the revolutionary matching tool designed to seamlessly connect aspiring 
          software engineers with the opportunities that align with their passions and expertise. 
          Imagine a platform where students can effortlessly bridge the gap between their academic knowledge and real-world application, 
          all while receiving mentorship, gaining valuable internship experience, and even securing potential job opportunities. 
          SkillSync goes beyond traditional job boards by intricately analyzing a user's skills, interests, and educational background, 
          offering a curated selection of companies eager to provide mentorship and hands-on experience. By facilitating this symbiotic relationship, 
          SkillSync transforms the job search experience into a tailored journey, ensuring that students not only find opportunities but also thrive in 
          environments that foster growth. Elevate your career prospects with<br /> <br / >SkillSync – where your skills meet their match!</b></Paragraph>
        </Container>
      </div>

       <Carousel />
      

      <div className="grey-green-background" style={{ height: "90vh" }}>
        <Container>
          <Title level={2}>Third Title</Title>
          <Paragraph>First paragraph of text</Paragraph>
          <Paragraph>Second paragraph of text</Paragraph>
        </Container>
      </div>
    </>
  );
}
