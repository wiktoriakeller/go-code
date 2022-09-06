const validateLength = (value: string, minLength: number, maxLength: number, errorMessage: string): [boolean, string] => {
    if(value.length >= minLength && value.length <= maxLength) {
        return [true, ""];
    }

    return [false, errorMessage];
}

export { validateLength };
