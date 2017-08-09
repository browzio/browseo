/* This file is auto-generated, see gen-event-enum.py.  */

#ifndef mozilla_TelemetryEventEnums_h
#define mozilla_TelemetryEventEnums_h
namespace mozilla {
namespace Telemetry {
namespace EventID {

// category: telemetry.test.second
enum class TelemetryTestSecond : uint32_t {
  Test_Object1 = 20,
  Test_Object2 = 21,
  Test_Object3 = 22,
};

// category: telemetry.test
enum class TelemetryTest : uint32_t {
  Test1_Object1 = 9,
  Test1_Object2 = 10,
  Test2_Object1 = 11,
  Test2_Object2 = 12,
  Optout_Object1 = 13,
  Optout_Object2 = 14,
  ExpiredVersion_Object1 = 15,
  ExpiredVersion_Object2 = 16,
  ExpiredDate_Object1 = 17,
  ExpiredDate_Object2 = 18,
  NotExpiredOptout_Object1 = 19,
};

// category: navigation
enum class Navigation : uint32_t {
  Search_AboutHome = 0,
  Search_AboutNewtab = 1,
  Search_Contextmenu = 2,
  Search_Oneoff = 3,
  Search_Suggestion = 4,
  Search_Alias = 5,
  Search_Enter = 6,
  Search_Searchbar = 7,
  Search_Urlbar = 8,
};

const uint32_t EventCount = 23;

} // namespace EventID
} // namespace mozilla
} // namespace Telemetry
#endif // mozilla_TelemetryEventEnums_h

