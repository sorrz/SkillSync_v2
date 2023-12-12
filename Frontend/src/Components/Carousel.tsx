import React from "react";
import { Carousel } from "react-bootstrap";

const MyCarousel = () => {
  return (
    <>
    <Carousel>
      <div id="carouselExampleSlidesOnly" className="carousel slide" data-ride="carousel">
  <div className="carousel-inner">
    <div className="carousel-item active" style={{ height: "90vh" }}>
      <img className="d-block w-100" src="/assets/herobanner.jpg"  alt="" />
      <title>Connecting Ambitious Interns with Industry leaders, SkillSync is a step to success. </title>
      <p>With vast corporate partnerships and personalized matches, SkillSync ensures you're connected with the perfect internship opportunities to propel your career trajectory. </p>
    </div>
    <div className="carousel-item" style={{ height: "90vh"}}>
      <img className="d-block w-100 bg-dark" src='' alt="Second slide" />

      <p>Connecting Ambitious Interns with Industry leaders, SkillSync is a step to success. </p>
    </div>
    <div className="carousel-item" style={{ height: "90vh" }}>
      <img className="d-block w-100" src="..." alt="Third slide" />
      <p>dummy text</p>
    </div>
  </div>

  <a className="carousel-control-prev" href="#carouselExampleControls" role="button" data-slide="prev">
    <span className="carousel-control-prev-icon" aria-hidden="true"></span>
    <span className="sr-only">Previous</span>
  </a>
  <a className="carousel-control-next" href="#carouselExampleControls" role="button" data-slide="next">
    <span className="carousel-control-next-icon" aria-hidden="true"></span>
    <span className="sr-only">Next</span>
  </a>

</div>
</Carousel>
    </>
  );
};

export default MyCarousel;
