import { describe, it, afterEach, assert } from 'vitest';
import { render, cleanup, screen } from '@testing-library/react';
import { BrowserRouter as Router } from 'react-router-dom';
import Footer from '../Components/Footer';

describe('Footer Component', () => {
  afterEach(() => {
    cleanup();
  });

  it('renders Footer component with correct content', () => {
    render(
      <Router>
        <Footer />
      </Router>
    );

    const footerElement = screen.getByTestId('footer-id');
    assert.isNotNull(footerElement);

    const footerContainer = screen.getByTestId('footer-container');
    assert.isNotNull(footerContainer);

    const homeLink = screen.getByText('Home');
    assert.isNotNull(homeLink);

    const aboutLink = screen.getByText('About');
    assert.isNotNull(aboutLink);

    // You can add more assertions for other elements as needed
  });
});

