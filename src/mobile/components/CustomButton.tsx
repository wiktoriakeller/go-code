import { Pressable, Text, StyleSheet, GestureResponderEvent } from 'react-native'
import React from 'react'

interface IButtonProps {
    text: string;
    onPress: (event: GestureResponderEvent) => void;
}

const CustomButton = (props: IButtonProps) => {
  return (
    <Pressable onPress={props.onPress} style={styles.container}>
      <Text style={styles.text}>{props.text}</Text>
    </Pressable>
  )
}

const styles = StyleSheet.create({
    container: {
        backgroundColor: 'cadetblue',
        borderColor: 'cadetblue',

        width: '88%',
        borderRadius: 5,
        padding: 12,
        marginVertical: 8,
        alignItems: 'center'
    },
    text: {
        color: 'white',
        fontFamily: 'sans-serif',
        fontWeight: 'bold',
        fontSize: 18
    }
});

export { IButtonProps, CustomButton };
