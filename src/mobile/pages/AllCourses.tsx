import { View } from "react-native"
import React, { useEffect, useState } from "react"
import { CourseListItem } from "../components/courses/CourseListItem"
import { ICourse, getAllCourses, IGetAllCoursesResponse } from "../api/courses/getAllCourses";
import { IApiResponse } from "../api/common";
import Spinner from "react-native-loading-spinner-overlay/lib";

export const AllCourses = () => {
  const [loading, setLoading] = useState(true);
  const [courses, setCourses] = useState<ICourse[]>([]);
  
  useEffect(() => {
    getAllCourses()
    .then((response: IApiResponse<IGetAllCoursesResponse>) => setCourses(response.data?.courses as ICourse[]))
    .catch((error: IApiResponse<IGetAllCoursesResponse>) => console.log(error))
    .finally(() => {
      setLoading(false);
    })
  }, []);

  return (
    <View>
      <Spinner
        visible={loading}
        textContent={""}
      />
      {
        courses.map((item) => 
          <CourseListItem
            {...item}
          />
        )
      }
    </View>
  )
}
