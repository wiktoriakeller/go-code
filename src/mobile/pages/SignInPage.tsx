import { View, Text, StyleSheet, GestureResponderEvent } from 'react-native'
import React, { useState } from 'react'
import { ICustomInputProps, CustomInput } from '../components/CustomInput';
import { IButtonProps, CustomButton } from '../components/CustomButton';

const SignInScreen = () => {
    const [email, setEmail] = useState('');
    const emailInput = {
        text: email,
        placeholder: 'Email',
        secureText: false,
        onChangeText: (value: string) => setEmail(value)
    };

    const [password, setPassword] = useState('');
    const passwordInput: ICustomInputProps = {
        text: password,
        placeholder: 'Password',
        secureText: true,
        onChangeText: (value: string) => setPassword(value)
    };

    const loginButton: IButtonProps = {
        text: 'Sign in',
        onPress: (event: GestureResponderEvent) => {
            console.warn("Sign in");
        }
    };

    return (
        <View style={styles.root}>
            <Text style={styles.logoText}>Go Code</Text>
            <CustomInput {...emailInput} />
            <CustomInput {...passwordInput}/>
            <CustomButton {...loginButton} />
        </View>
    )
}

const styles = StyleSheet.create({
    root: {
        flex: 1,
        alignItems: 'center',
        justifyContent: 'flex-start',
    },
    logoText: {
        marginTop: '25%',
        marginBottom: '15%',
        color: 'cadetblue',
        fontFamily: 'sans-serif',
        fontSize: 48,
        fontWeight: 'bold',
        fontStyle: 'italic'
    }
});

export default SignInScreen;