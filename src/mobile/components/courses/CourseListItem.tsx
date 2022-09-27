import { View, Text, StyleSheet, ScrollView } from "react-native";
import React from "react";
import { TagItem } from "./TagItem";

export const CourseListItem = () => {
  return (
    <ScrollView style={styles.root}>
      <View style={styles.mainInfo}>
        <Text style={styles.title}>Course name</Text>
        <View style={styles.secondaryInfo}>
          <TagItem
            text="35 XP"
          />
          <TagItem
            text="Treshold: 70%"
          />
        </View>
      </View>
      <Text>Short description</Text>
    </ScrollView >
  )
}

const styles = StyleSheet.create({
  root: {
    borderRadius: 15,
    backgroundColor: "pink",
    marginTop: 12,
    marginLeft: "6%",
    marginRight: "6%",
    paddingTop: "2%",
    paddingLeft: "4%",
    paddingBottom: "2%",
    paddingRight: "4%"
  },
  mainInfo: {
    flex: 1,
    flexDirection: "row",
    justifyContent: "flex-start",
    alignItems: "baseline"
  },
  secondaryInfo: {
    flex: 1,
    flexDirection: "row",
    justifyContent: "flex-end",
  },
  title: {
    fontWeight: "bold",
    fontSize: 18
  }
});
