import { View, TextInput, StyleSheet } from 'react-native'

interface ICustomInputProps {
    text: string;
    placeholder: string;
    secureText: boolean;
    onChangeText: (text: string) => void;
}

const CustomInput = (props: ICustomInputProps) => {
    return (
        <View style={styles.container}>
            <TextInput
                value={props.text}
                onChangeText={props.onChangeText}
                style={styles.input}
                placeholder={props.placeholder}
                secureTextEntry={props.secureText}    
            />
        </View>
    )
}

const styles = StyleSheet.create({ 
    container: {
        backgroundColor: 'white',
        width: '88%',

        borderColor: '#e8e8e8',
        borderWidth: 1,
        borderRadius: 5,

        paddingHorizontal: 12,
        marginVertical: 8,
        height: 50,
        textAlign: 'center'
    },
    input: {
        flex: 1,
        justifyContent: 'flex-start',
        fontSize: 16
    }
});

export { ICustomInputProps, CustomInput };
