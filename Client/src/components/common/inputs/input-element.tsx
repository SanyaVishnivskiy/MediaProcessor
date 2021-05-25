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
        <div>
            <label htmlFor={props.id}>{props.label}</label>
            <input
                type={props.inputType}
                name={props.id} 
                onChange={e => props.onChange(e.target.value)}
                disabled={props.disabled}
                value={props.value} />
        </div>
    );
}