import { Typography } from "antd";
import { Container } from "react-bootstrap";
import GalleryCard3 from "../Components/GalleryCard3";
import "../Styles/homes.css";
import { useNavigate } from "react-router-dom";

const { Title, Paragraph } = Typography;


export function Home() {

  const navigate = useNavigate();
  const handleLoginClick = () => {
    navigate("/login")
  }
  const handleRegisterClick = () => {
    navigate("/register")
  }

  return (
    <>
   
      <div className="hero">
      <div className="overlay">
        <Container className="hero-text" >
          <Title>SkillSync</Title>
          <Paragraph> Synchronizing Talent With Opportunity<br /> 
          <br></br>
         <p>A revolutionary matching tool designed to seamlessly connect aspiring 
          software engineers with the opportunities that align with their passions and expertise. 
          Imagine a platform where students can effortlessly bridge the gap between their academic knowledge and real-world application, 
          all while receiving mentorship, gaining valuable internship experience, and even securing potential job opportunities. <br></br>
          SkillSync goes beyond traditional job boards by intricately analyzing a user's skills, interests, and educational background, 
          offering a curated selection of companies eager to provide mentorship and hands-on experience. By facilitating this symbiotic relationship, 
          SkillSync transforms the job search experience into a tailored journey, ensuring that students not only find opportunities but also thrive in 
          environments that foster growth. Elevate your career prospects with <b>SkillSync</b> – where your skills meet their match!</p>
          <br></br>
          <br></br>
          <button className="register-button" onClick={handleLoginClick}>Get started</button>
          </Paragraph>

        </Container>
        </div>
      </div>

       {/* Gallery Section */}
       <div className="gallery">
          <div className="gallery1">
            <h1 className="gallery-heading heading2">Explore Our Journey</h1>
            <span className="gallery-sub-heading">
              Take a look at some snapshots of our website
            </span>
            <div className="container5">
              {/* Gallery Cards */}
              <GalleryCard3
                image_src="https://images.unsplash.com/photo-1423666639041-f56000c27a9a?crop=entropy&amp;cs=tinysrgb&amp;fit=max&amp;fm=jpg&amp;ixid=M3w5MTMyMXwwfDF8cmFuZG9tfHx8fHx8fHx8MTcwMjMwNDU3NHw&amp;ixlib=rb-4.0.3&amp;q=80&amp;w=400"
                rootClassName="rootClassName"
              />
              <GalleryCard3
                image_src="https://images.unsplash.com/photo-1499244571948-7ccddb3583f1?crop=entropy&amp;cs=tinysrgb&amp;fit=max&amp;fm=jpg&amp;ixid=M3w5MTMyMXwwfDF8cmFuZG9tfHx8fHx8fHx8MTcwMjMwNDU3NHw&amp;ixlib=rb-4.0.3&amp;q=80&amp;w=400"
                rootClassName="rootClassName1"
              />
              <GalleryCard3
                image_src="https://images.unsplash.com/photo-1469571486292-0ba58a3f068b?crop=entropy&amp;cs=tinysrgb&amp;fit=max&amp;fm=jpg&amp;ixid=M3w5MTMyMXwwfDF8cmFuZG9tfHx8fHx8fHx8MTcwMjMwNDU3NHw&amp;ixlib=rb-4.0.3&amp;q=80&amp;w=400"
                rootClassName="rootClassName3"
              />
              <GalleryCard3
                image_src="https://images.unsplash.com/photo-1516321318423-f06f85e504b3?crop=entropy&amp;cs=tinysrgb&amp;fit=max&amp;fm=jpg&amp;ixid=M3w5MTMyMXwwfDF8cmFuZG9tfHx8fHx8fHx8MTcwMjMwNDU3NHw&amp;ixlib=rb-4.0.3&amp;q=80&amp;w=400"
                rootClassName="rootClassName2"
              />
             
              <GalleryCard3
                image_src="https://images.unsplash.com/photo-1522071820081-009f0129c71c?crop=entropy&amp;cs=tinysrgb&amp;fit=max&amp;fm=jpg&amp;ixid=M3w5MTMyMXwwfDF8cmFuZG9tfHx8fHx8fHx8MTcwMjMwNDU3NHw&amp;ixlib=rb-4.0.3&amp;q=80&amp;w=400"
                rootClassName="rootClassName5"
              />
              <GalleryCard3
                image_src="https://images.unsplash.com/photo-1432821596592-e2c18b78144f?crop=entropy&amp;cs=tinysrgb&amp;fit=max&amp;fm=jpg&amp;ixid=M3w5MTMyMXwwfDF8cmFuZG9tfHx8fHx8fHx8MTcwMjMwNDU3NHw&amp;ixlib=rb-4.0.3&amp;q=80&amp;w=400"
                rootClassName="rootClassName6"
              />
              <GalleryCard3
                image_src="https://images.unsplash.com/photo-1502139214982-d0ad755818d8?crop=entropy&amp;cs=tinysrgb&amp;fit=max&amp;fm=jpg&amp;ixid=M3w5MTMyMXwwfDF8cmFuZG9tfHx8fHx8fHx8MTcwMjMwNDU3NHw&amp;ixlib=rb-4.0.3&amp;q=80&amp;w=400"
                rootClassName="rootClassName7"
              />
              <GalleryCard3
                image_src="https://images.unsplash.com/photo-1491438590914-bc09fcaaf77a?crop=entropy&amp;cs=tinysrgb&amp;fit=max&amp;fm=jpg&amp;ixid=M3w5MTMyMXwwfDF8cmFuZG9tfHx8fHx8fHx8MTcwMjMwNDU3NHw&amp;ixlib=rb-4.0.3&amp;q=80&amp;w=400"
                rootClassName="rootClassName8"
              />
            
              <GalleryCard3
                image_src="https://images.unsplash.com/photo-1557804506-669a67965ba0?crop=entropy&amp;cs=tinysrgb&amp;fit=max&amp;fm=jpg&amp;ixid=M3w5MTMyMXwwfDF8cmFuZG9tfHx8fHx8fHx8MTcwMjMwNDU3NHw&amp;ixlib=rb-4.0.3&amp;q=80&amp;w=400"
                rootClassName="rootClassName11"
              />
            </div>
          </div>
        </div>
      

      <div className="grey-green-background grey-green-container" style={{ height: "80vh", padding:"50px" }}>
        <Container className="third-container" >
          <Title level={2} className="mulish-font">Build A Powerful Network Early</Title>
          <Paragraph className="mulish-font">Effective networking plays a pivotal role in shaping your career trajectory. By establishing connections with 
          CEOs, industry leaders, and forward-thinking entrepreneurs, you lay the foundation for a robust professional network early in your journey.
          Our extensive corporate partnerships cut across diverse industries, offering interns an abundance of opportunities to forge meaningful connections.<br></br>
          Interns, through exclusive access to company events, gain a firsthand experience of the industry landscape and open avenues for networking.
          The networking opportunities extend beyond the internship period, providing a platform for ongoing engagement with professionals.
          It's not just about attending events; it's about building relationships that extend beyond the surface. Many of our interns have successfully 
          transitioned into full-time roles post-internship, attributing their success to the valuable connections nurtured during their time with us.
          Embrace the power of networking, and let it be a guiding force in your journey toward professional excellence.





</Paragraph>
          <br></br>
          
          <Title level={3} className="mulish-font">Embrace Success: Start Your Journey With Us </Title>
          <Paragraph> 
          Join the dynamic and diverse community of SkillSync, and we'll ensure you get the internship opportunities that coincide perfectly with your career aspirations. Let's weave your success story together. </Paragraph>
          <br></br><br></br>
          <button className="register-button" onClick={handleRegisterClick}>Join SkillSync Today!</button>
         
        </Container>
      </div>
      
    </>
  );
}
