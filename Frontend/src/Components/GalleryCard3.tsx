// GalleryCard3.tsx

import React from "react";

interface GalleryCardProps {
  image_src: string;
  rootClassName: string;
}

const GalleryCard3: React.FC<GalleryCardProps> = ({ image_src, rootClassName }) => {
  return (
    <div className={rootClassName}>
      <img src={image_src} alt="Gallery Card" className="gallery-card-image" />
      {/* Add any additional content or styling you want for each GalleryCard */}
    </div>
  );
};

export default GalleryCard3;
