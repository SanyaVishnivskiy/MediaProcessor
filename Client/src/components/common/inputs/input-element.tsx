import { Form } from "react-bootstrap";

interface RenderInputProps {
    id: string,
    label: string,
    value: string,
    inputType: string,
    disabled?: boolean
    onChange: (value: string) => void
}

export const InputElement = (props: RenderInputProps) => {
    return (
        <Form.Group controlId={props.id}>
            <Form.Label>{props.label}</Form.Label>
            <Form.Control
                type={props.inputType}
                name={props.id} 
                onChange={e => props.onChange(e.target.value)}
                disabled={props.disabled}
                value={props.value} />
        </Form.Group>
    );
}