interface RenderInputProps {
    id: string,
    label: string,
    value: string,
    inputType: string,
    onChange: (value: string) => void
}

export const ActionInputElement = (props: RenderInputProps) => {
    return (
        <div>
            <label htmlFor={props.id}>{props.label}</label>
            <input type={props.inputType} name={props.id} 
                onChange={e => props.onChange(e.target.value)}
                value={props.value} />
        </div>
    );
}