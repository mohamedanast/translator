import React from "react";
import Dropdown from 'react-bootstrap/Dropdown';
import { Actions } from './constants';

class LangSelector extends React.Component {

    onChange = this.onChange.bind(this);

    onChange(lang) {
        this.props.onSelect(Actions.CHANGE_LANG, lang);
    }

    render() {
        const languages = this.props.languages.map((lang, idx) => {
            return (
                <Dropdown.Item key={lang.shortName} onClick={() => this.onChange(lang)}
                    active={lang.shortName === this.props.currentLang ? true : false} >
                    {lang.fullName}
                </Dropdown.Item>
            )
        });

        return <Dropdown>
            <Dropdown.Toggle variant="outline-primary" id="dropdown-basic" className="w-50">
                {this.props.currentLang && <span>{this.props.currentLang.fullName}</span>}
                {!this.props.currentLang && <span>Choose language</span>}
            </Dropdown.Toggle>
            <Dropdown.Menu>{languages}</Dropdown.Menu>
        </Dropdown>;
    }
}

export default LangSelector;