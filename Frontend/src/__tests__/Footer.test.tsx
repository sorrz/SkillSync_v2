import { test, assert } from 'vitest';
import { describe, afterEach } from 'vitest';
import { render, screen, cleanup } from '@testing-library/react';
import Footer from '../Components/Footer';


describe('Footer Component', () => {
    afterEach(() => {
      cleanup();
    });


test('renders Footer component correct', async () => {
  render(<Footer />);

  const footer = screen.queryByRole('contentinfo');
  assert.isNotNull(footer);
});

test('Footer contains correct content', async () => {
    render(<Footer />);

    const container = screen.getByTestId('footer-container');

    assert.isNotNull(container);
    assert.equal(container.classList.contains('bg-light'), true);
    assert.equal(container.innerText.includes(`© ${new Date().getFullYear()} Your Company Name`), true);
  });

});