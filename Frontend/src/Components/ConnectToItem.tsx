import React from 'react';
import { Button } from 'antd';
import { HeartFilled, HeartOutlined } from '@ant-design/icons';

interface ConnectToProps {
  onLike: () => void;
  isLiked: boolean;
}

const ConnectToItem: React.FC<ConnectToProps> = ({ onLike, isLiked }) => {
  return (
    <Button type="primary" icon={isLiked ? <HeartFilled /> : <HeartOutlined />} onClick={onLike}>
      {isLiked ? 'You have shown interest in' : 'Click to Show interest in'}
    </Button>
  );
};

export default ConnectToItem;
