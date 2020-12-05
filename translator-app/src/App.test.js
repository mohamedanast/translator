import { render, screen } from '@testing-library/react';
import ReactDOM from 'react-dom';
import App from './App';

test('renders learn react link', () => {
  render(<App />);
  const headerElement = screen.getByText(/Translator/i);
  expect(headerElement).toBeInTheDocument();
});

it('renders without crashing', () => {
    const div = document.createElement('div');
    ReactDOM.render(<App />, div);
});