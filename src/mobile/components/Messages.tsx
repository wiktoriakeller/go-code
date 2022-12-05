import React from "react";
import { StyleSheet, Text, View } from "react-native";
import colors from "../styles/colors";

interface IMessagesProps {
  messages: string[];
  isError: boolean;
}

function Messages(props: IMessagesProps) {
  const colorStyle = {
    backgroundColor: colors.errorBlock,
  };

  if (!props.isError) {
    colorStyle.backgroundColor = colors.successBlock;
  }

  return (
    <View style={styles.root}>
      {props.messages.map((message) => (
        <Text style={[styles.text, colorStyle]}>{message}</Text>
      ))}
    </View>
  );
}

const styles = StyleSheet.create({
  root: {
    margin: "2%",
  },
  text: {
    color: colors.white,
    fontSize: 18,
    margin: 4,
    textAlign: "center",
    padding: 4,
    borderRadius: 5,
  },
});

export { IMessagesProps, Messages };
