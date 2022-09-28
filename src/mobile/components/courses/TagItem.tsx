import { View, Text, StyleSheet } from "react-native";
import React from "react";

interface ITagProps {
  text: string;
}

const TagItem = (props: ITagProps) => {
  return (
    <View style={styles.root}>
      <Text style={styles.text}>{props.text}</Text>
    </View>
  )
}

const styles = StyleSheet.create({
  root: {
    borderRadius: 15,
    backgroundColor: "yellow",
    paddingTop: 2,
    paddingBottom: 2,
    paddingLeft: 5,
    paddingRight: 5,
    marginLeft: 5
  },
  text: {
    fontSize: 12
  }
});

export { ITagProps, TagItem }
