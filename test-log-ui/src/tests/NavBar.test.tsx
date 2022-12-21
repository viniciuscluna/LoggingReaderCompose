import { render, screen } from '@testing-library/react';

import NavBar from '../components/NavBar';

describe('App', () => {
  it('renders headline', () => {
    render(<NavBar />);

    expect(screen.getByText('LogSearch - Sample')).toBeInTheDocument();
  });
});