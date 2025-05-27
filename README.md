# 🐣 FreeChicken - Unity 기반 3인칭 캐주얼 액션 어드벤처

> 사용자가 직접 조작하는 병아리가 장애물을 피해 성장해 나가는 3인칭 어드벤처 게임입니다.  
> Unity 및 C#을 기반으로 전체 클라이언트 시스템을 설계하고 구현했습니다.

---

## 🧩 프로젝트 개요

| 항목             | 내용                                                        |
|------------------|-------------------------------------------------------------|
| 플랫폼           | Unity (PC/Mobile 지원)                                      |
| 엔진             | Unity 2021.3.x (URP 환경)                                   |
| 개발 역할        | 클라이언트 개발 100% 담당                                   |
| 주요 기능        | Save/Load 시스템, 상태 기반 FSM, 시네머신 전환, 광고 연동 등 |
| 프로젝트 타입    | 개인 프로젝트 (기획 ~ 구현 ~ 배포까지 전 과정 직접 수행)    |

---

## 📁 주요 기능 및 구조

### 🎮 1. 플레이어 변신 시스템 (Input 기반 FSM) /EventSystem/
- 특정 Zone에 진입 시 `F` 키 입력 → `Egg Mode`로 변신
- 3초간 대기 후 확정 상태 전환, `E` 키로 중간 취소 가능
- 시네머신 기반 카메라 자동 전환
- UI 및 상태값, 입력 동기화 처리
- 상태에 따라 카메라 우선순위 자동 전환
  
> 핵심 스크립트: `EggModeController.cs`, `FactoryManagerController.cs`, `CameraManager.cs`

---

### 💾 2. Save & Load 시스템 /SaveSystem/
- 난이도(Easy/Hard) 별 JSON 저장 구조
- `GameSave.EasyLevel` / `HardLevel` 전역 변수로 레벨 유지
- 씬 전환 및 중복 진입 시 무결성 보장

> 핵심 스크립트: `GameSave.cs`, `PlayerDataManager.cs`

---

### 💬 3. 몰입형 Dialog System (타이핑 애니메이션 + 다국어 지원) /DialogueSystem/
- Queue 기반의 대사 순차 출력 시스템
- Coroutine으로 타이핑 애니메이션 연출 → 한 글자씩 출력
- 스페이스바, 마우스 클릭 등 다양한 입력 방식 대응
- 영어/한글 UI 자동 전환 (PlayerData.language 기반) 시 자동 전환 및 유저 제어 구분

> 핵심 스크립트: `FactoryUIManager.cs`

---

### 📢 4. Google AdMob 전면 광고 연동 /AdManager/
- GoogleMobileAds SDK 사용
- 광고 로딩, 실패 시 재시도, 종료 시 다음 동작 연결
- 테스트 ID 기반, 실 서비스 아님

> 핵심 스크립트: `InterstitialAdManager.cs`

---

### 🎯 5. 장애물 Enum + OOP 설계 /ObstacleSystem/
- `MoveObstacleType` 기반의 행동 클래스 분리
- `ObstacleBehaviorFactory`로 전략 패턴 구현
- 개별 동작은 `IObstacleBehavior` 인터페이스로 통합

> 핵심 스크립트: `MovingObstacle.cs`, `ObstacleBehaviorFactory.cs`

---
### 🏃‍♂️ 6. CityRunner: Endless 랜드 연결 구조 /CityRunner/
- 총 6종의 랜드 프리팹을 무한히 연결하는 Endless 구조
- 랜드가 생성될 때마다 Skybox가 중복되지 않게 랜덤 변경됨
- 일정 거리 이상 벗어난 랜드는 Object Pool 구조를 통해 재활용
> 핵심 스크립트: `CitySceneSpawn.cs`, `CityArea.cs`, `ObjectPool.cs`

---

## 🔧 기술 스택

- Unity 2021.3 (URP)
- C#
- Cinemachine
- GoogleMobileAds SDK
- JSON (PlayerPrefs 대신 파일 기반 저장)

---

## 📌 참고

> 해당 프로젝트는 **포트폴리오 제출용 목적에 맞춰 재구성된 프로젝트**이며,  
> **실 상용 게임 코드가 아닌 구조 설계 및 기능 구현에 집중된 예제코드**로 구성되어 있습니다.
