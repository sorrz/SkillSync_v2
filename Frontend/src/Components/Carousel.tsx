
const Carousel = () => {
  return (
    <>
      <div id="carouselExampleSlidesOnly" className="carousel slide" data-ride="carousel">
  <div className="carousel-inner">
    <div className="carousel-item active" style={{ height: "90vh" }}>
      <img className="d-block w-100" src="..." alt="First slide" />
    </div>
    <div className="carousel-item" style={{ height: "90vh"}}>
      <img className="d-block w-100 bg-dark" src="..." alt="Second slide" />
    </div>
    <div className="carousel-item" style={{ height: "90vh" }}>
      <img className="d-block w-100" src="..." alt="Third slide" />
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
    </>
  );
};

export default Carousel;
