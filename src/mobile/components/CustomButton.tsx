import { Pressable, Text, StyleSheet, GestureResponderEvent, StyleProp, ViewStyle } from "react-native"
import React from "react"
import colors from "../styles/colors"

interface IButtonProps {
  text: string;
  containerStyle?: any;
  textStyle?: any;
  onPress: (event: GestureResponderEvent) => void;
}

const CustomButton = (props: IButtonProps) => {
  return (
    <Pressable onPress={props.onPress} style={[styles.container, props.containerStyle ?? {}]}>
      <Text style={[styles.text, props.textStyle ?? {}]}>{props.text}</Text>
    </Pressable>
  )
}

const styles = StyleSheet.create({
  container: {
    backgroundColor: colors.primary,
    borderColor: colors.primary,
    width: "88%",
    borderRadius: 5,
    padding: 12,
    marginVertical: 8,
    alignItems: "center"
  },
  text: {
    color: colors.white,
    fontFamily: "sans-serif",
    fontWeight: "bold",
    fontSize: 18
  }
});

export { IButtonProps, CustomButton };
