import logo from './logo.svg';
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'react-bootstrap'
import Footer from "./componenets/Footer"
import Translator from "./componenets/Translator"

function App() {
    return (
        <div className="App container">
            <header className="App-header">
                <img src={logo} className="App-logo" alt="logo" />
                <h1 className="App-title display-2 my-2">Translator</h1>
            </header>
            <Translator />
            <Footer />
        </div>
    );
}

export default App;
