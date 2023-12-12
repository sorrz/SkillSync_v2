import "../Styles/about.css";
export function About(){

    return (
        <div className="about-container mulish-font">
        <h1 className="title-about">About SkillSync</h1>
  
        <section className="about-section">
          <img src="src\assets\dans_logo_png.png"alt="SkillSync Logo" className="about-image" />
          <div className="about-text">
            <p>
              SkillSync is a revolutionary platform that places a strong emphasis on networking. Our mission
              is to connect individuals with CEOs, industry leaders, and innovative entrepreneurs, allowing them
              to build a robust professional network right from the start of their careers.
            </p>
          </div>
        </section>
  
        <section className="about-section">
          <img src="src\assets\networking.jpg" alt="Networking Event" className="about-image" />
          <div className="about-text">
            <p>
              SkillSync organizes exclusive networking events where interns can interact with key industry figures.
              These events provide a unique opportunity for skill development, knowledge sharing, and relationship building.
            </p>
          </div>
        </section>
  
        <section className="about-section">
          <img src="src\assets\partnership.jpg" alt="Corporate Partnerships" className="about-image" />
          <div className="about-text">
            <p>
              Our extensive corporate partnerships span across various industries, offering interns a diverse range
              of opportunities. These partnerships ensure that interns gain valuable insights and exposure to different
              sectors during their SkillSync journey.
            </p>
          </div>
        </section>
  
        <section className="about-section">
          <img src="src\assets\succes.jpg"alt="Success Stories" className="about-image" />
          <div className="about-text">
            <p>
              The success stories of our interns speak volumes. Many of our interns have transitioned seamlessly
              into full-time roles post their SkillSync internships, attributing their success to the connections
              they made and the experiences gained on our platform.
            </p>
          </div>
        </section>
      </div>
 );
}